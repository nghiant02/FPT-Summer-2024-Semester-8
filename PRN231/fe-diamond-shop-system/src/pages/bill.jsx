import { Helmet } from 'react-helmet-async';

import { BillView } from 'src/sections/bill/view';

// ----------------------------------------------------------------------

export default function BillPage() {
  return (
    <>
      <Helmet>
        <title> Bill </title>
      </Helmet>

      <BillView />
    </>
  );
}
