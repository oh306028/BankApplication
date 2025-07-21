import { useEffect, useState } from "react";
import AuthenticationService from "../Authentication/AuthenticationService";
import ClientAccounts from "./ClientAccounts";
function BankAccounts() {
  const [isAdmin, setIsAdmin] = useState<boolean>(false);

  useEffect(() => {
    const checkAdmin = async () => {
      const result = await AuthenticationService.isAdmin();
      setIsAdmin(result);
    };

    checkAdmin();
  }, []);

  return (
    <>{isAdmin ? <h1>Zalogowano administratora</h1> : <ClientAccounts />}</>
  );
}

export default BankAccounts;
