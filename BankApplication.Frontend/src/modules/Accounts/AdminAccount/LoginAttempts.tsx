import { useEffect, useState } from "react";
import AccountsService, { type LoginAttemptDetails } from "../AccountsService";

function LoginAttempts() {
  const [data, setData] = useState<LoginAttemptDetails[]>();
  useEffect(() => {
    const fetchClients = async () => {
      const result = await AccountsService.getLoginAttempts();
      setData(result);
    };
    fetchClients();
  }, []);
  return <></>;
}

export default LoginAttempts;
