import { useEffect, useState } from "react";
import TransferService, { type Details } from "./TransferService";
import dayjs from "dayjs";

interface TransferListProps {
  publicId: string;
}

const TransferList: React.FC<TransferListProps> = ({ publicId }) => {
  const [details, setDetails] = useState<Details[]>();

  useEffect(() => {
    const fetchdata = async () => {
      const data = await TransferService.getList(publicId);
      setDetails(data);
    };
    fetchdata();
  }, []);

  return (
    <>
      <p>Wszystkie</p>
      <ul>
        {details?.map((p) => (
          <li>
            <p>
              Adresat: {p.sender} <span>{p.senderNumber}</span>{" "}
            </p>
            <p>
              Odbiorca: {p.receiver} <span>{p.receiverNumber}</span>{" "}
            </p>
            <p>Tytuł: {p.title} </p>
            {p.descrition && <p>Opis: {p.descrition} </p>}
            <p>Kwota: {p.amount.toFixed(2)} </p>
            <p>
              Data transferu: {dayjs(p.transferDate).format("DD.MM.YYYY HH:mm")}
            </p>
          </li>
        ))}
      </ul>
    </>
  );
};

export default TransferList;
