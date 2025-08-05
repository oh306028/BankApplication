import { useEffect, useState } from "react";
import AccountsService, { type ClientDetails } from "../AccountsService";

function AdminList() {
  const [data, setData] = useState<ClientDetails[]>();
  useEffect(() => {
    const fetchClients = async () => {
      const result = await AccountsService.getAdminList();
      setData(result);
    };
    fetchClients();
  }, []);
  return (
    <>
      {data?.map((p) => (
        <li>
          <p>{p.fullName}</p>
          <p>{p.email}</p>
          <p>{p.pesel}</p>
          <p>{p.phone}</p>
          <div>
            <p>{p.country}</p>
            <p>{p.city}</p>
            <p>{p.postalCode}</p>
            <p>{p.number}</p>
          </div>
        </li>
      ))}
    </>
  );
}

export default AdminList;
