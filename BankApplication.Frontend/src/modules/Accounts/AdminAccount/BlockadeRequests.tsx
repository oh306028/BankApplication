import { useEffect, useState } from "react";
import AccountsService, { type BlockRequestDetails } from "../AccountsService";

function BlockadeRequests() {
  const [data, setData] = useState<BlockRequestDetails[]>();
  useEffect(() => {
    const fetchClients = async () => {
      const result = await AccountsService.getBlockRequests();
      setData(result);
    };
    fetchClients();
  }, []);
  return <></>;
}

export default BlockadeRequests;
