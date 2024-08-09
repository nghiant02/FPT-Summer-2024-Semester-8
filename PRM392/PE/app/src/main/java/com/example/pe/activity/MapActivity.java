package com.example.pe.activity;

import android.os.Bundle;
import android.widget.TextView;
import android.widget.Toast;

import androidx.fragment.app.FragmentActivity;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.Volley;
import com.example.pe.R;
import com.example.pe.model.Tacgia;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.MarkerOptions;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class MapActivity extends FragmentActivity implements OnMapReadyCallback {

    private GoogleMap mMap;
    private Tacgia tacgia;
    private RequestQueue requestQueue;
    private TextView addressTextView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_map);

        tacgia = (Tacgia) getIntent().getSerializableExtra("tacgia");

        requestQueue = Volley.newRequestQueue(this);
        addressTextView = findViewById(R.id.address_text);

        SupportMapFragment mapFragment = (SupportMapFragment) getSupportFragmentManager()
                .findFragmentById(R.id.map);
        mapFragment.getMapAsync(this);

        geocodeAddress(tacgia.getDiaChi());
    }

    @Override
    public void onMapReady(GoogleMap googleMap) {
        mMap = googleMap;
    }

    private void geocodeAddress(String address) {
        String url = "https://nominatim.openstreetmap.org/search?q=" + address + "&format=json&addressdetails=1";

        JsonArrayRequest jsonArrayRequest = new JsonArrayRequest
                (Request.Method.GET, url, null, new Response.Listener<JSONArray>() {

                    @Override
                    public void onResponse(JSONArray response) {
                        try {
                            if (response.length() > 0) {
                                JSONObject location = response.getJSONObject(0);
                                double lat = location.getDouble("lat");
                                double lng = location.getDouble("lon");
                                String displayName = location.getString("display_name");

                                addressTextView.setText("Vĩ độ: " + lat + "\nKinh độ: " + lng + "\n" + displayName);

                                LatLng tacgiaLocation = new LatLng(lat, lng);
                                if (mMap != null) {
                                    mMap.addMarker(new MarkerOptions().position(tacgiaLocation).title(tacgia.getTenTacgia()));
                                    mMap.moveCamera(CameraUpdateFactory.newLatLngZoom(tacgiaLocation, 15));
                                }
                            } else {
                                Toast.makeText(MapActivity.this, "Không tìm thấy địa chỉ", Toast.LENGTH_SHORT).show();
                            }
                        } catch (JSONException e) {
                            e.printStackTrace();
                            Toast.makeText(MapActivity.this, "Không thể phân tích vị trí", Toast.LENGTH_SHORT).show();
                        }
                    }
                }, new Response.ErrorListener() {

                    @Override
                    public void onErrorResponse(VolleyError error) {
                        error.printStackTrace();
                        Toast.makeText(MapActivity.this, "Không thể lấy vị trí", Toast.LENGTH_SHORT).show();
                    }
                });

        requestQueue.add(jsonArrayRequest);
    }
}
