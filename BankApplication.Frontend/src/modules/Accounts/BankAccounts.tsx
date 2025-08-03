import { useEffect, useState } from "react";
import AuthenticationService from "../Authentication/AuthenticationService";
import ClientAccounts from "./ClientAccount/ClientAccounts";
import AdminAccounts from "./AdminAccount/AdminAccounts";
function BankAccounts() {
  const [isAdmin, setIsAdmin] = useState<boolean>(false);

  useEffect(() => {
    const checkAdmin = async () => {
      const result = await AuthenticationService.isAdmin();
      setIsAdmin(result);
    };

    checkAdmin();
  }, []);

  return <>{isAdmin ? <AdminAccounts /> : <ClientAccounts />}</>;
}

export default BankAccounts;
