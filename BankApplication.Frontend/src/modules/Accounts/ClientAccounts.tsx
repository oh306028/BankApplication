import { useEffect, useState } from "react";
import AccountsService from "./AccountsService";
import AccountsChoose from "./AccountsChoose";

function ClientAccounts() {
  const [hasAccounts, setHasAccounts] = useState<boolean>(false);

  useEffect(() => {
    const checkAdmin = async () => {
      const result = await AccountsService.hasBankAccounts();

      setHasAccounts(result);
    };

    checkAdmin();
  }, []);

  return <>{hasAccounts ? <h1>Pokazuje konta</h1> : <AccountsChoose />}</>;
}

export default ClientAccounts;
