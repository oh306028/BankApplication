import { useEffect, useState } from "react";
import AccountsService, { type KeyValueParir } from "./AccountsService";

function AccountsChoose() {
  const [bankAccountTypes, setBankAccountTypes] = useState<KeyValueParir[]>([]);

  useEffect(() => {
    const getAccountTypes = async () => {
      const types = await AccountsService.getTypes();
      setBankAccountTypes(types);
    };
    getAccountTypes();
  }, []);

  return (
    <>
      <h1>Utwórz swój pierwszy rachunek bankowy!</h1>
      <ul>
        {bankAccountTypes.map((p) => (
          <li key={p.key}>{p.name}</li>
        ))}
      </ul>
    </>
  );
}

export default AccountsChoose;
