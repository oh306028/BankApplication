import { useEffect, useState } from "react";
import AccountsService, { type Details } from "../AccountsService";

function BankAccountList() {
  const [data, setData] = useState<Details[]>();
  useEffect(() => {
    const fetchClients = async () => {
      const result = await AccountsService.getBankAccounts();
      setData(result);
    };
    fetchClients();
  }, []);
  return <></>;
}

export default BankAccountList;
