import { useEffect, useState } from "react";
import TransferService, { type Details } from "../Tranfers/TransferService";
import dayjs from "dayjs";

function AllTransferList() {
  const [data, setData] = useState<Details[]>();
  useEffect(() => {
    const fetchData = async () => {
      const result = await TransferService.getAll();
      setData(result);
    };
    fetchData();
  }, []);
  return (
    <>
      {data?.map((p) => (
        <li>
          <div>
            <span>{dayjs(p.transferDate).format("DD.MM.YYYY HH:mm")}</span>
            <span>{p.amount.toFixed(2)}</span>
          </div>
          <div>
            <p>{p.title}</p>
            <div>
              <p>
                {p.sender}
                {p.receiver}
              </p>
              <span>
                {p.senderNumber}
                {p.receiverNumber}
              </span>
            </div>
          </div>
        </li>
      ))}
    </>
  );
}

export default AllTransferList;
