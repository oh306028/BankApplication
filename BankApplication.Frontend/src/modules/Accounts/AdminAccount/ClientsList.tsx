import { useEffect, useState } from "react";
import AccountsService, { type ClientDetails } from "../AccountsService";

function ClientsList() {
  const [data, setData] = useState<ClientDetails[]>();
  useEffect(() => {
    const fetchClients = async () => {
      const result = await AccountsService.getClientList();
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

export default ClientsList;
