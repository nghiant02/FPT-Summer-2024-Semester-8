import { KEYS } from "../enums";


export const saveToken = (key:string) => {
  localStorage.setItem(KEYS.ACCESS_TOKEN, key);
};

export const loadToken = async () => {
  const token = await localStorage.getItem(KEYS.ACCESS_TOKEN);
  return null;
};

export const clearToken = () => {
  localStorage.clear();
};

export const logout = ()=>{
  clearToken();
  
}
