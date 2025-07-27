import { useEffect, useState } from "react";
import AccountsService, {
  type KeyValuePair,
  type Details,
} from "../AccountsService";
import ClientNavBar from "./ClientNavBar";
import SendTransfer from "../Tranfers/SendTransfer";

function Details() {
  const [ownTypes, setOwnTypes] = useState<KeyValuePair[]>([]);
  const [details, setDetails] = useState<Details>();
  const [isTransferActive, setIsTransferActive] = useState<boolean>(false);
  let publicId = "";

  const getOwnTypes = async () => {
    const result = await AccountsService.getOwnTypes();
    setOwnTypes(result);
  };

  const fetchDetails = async (type: string) => {
    const result = await AccountsService.getDetailsByType(type);
    publicId = result.publicId;
    setDetails(result);
  };

  const toggleTransfer = () => {
    setIsTransferActive(!isTransferActive);
  };

  useEffect(() => {
    getOwnTypes();
  }, []);

  useEffect(() => {
    if (ownTypes.length > 0) {
      fetchDetails(ownTypes[0].value);
    }
  }, [ownTypes]);

  return (
    <>
      <ClientNavBar />
      <div>
        <h1>Moje konta bankowe</h1>
        <div>
          <ul>
            {ownTypes.map((p) => (
              <li onClick={() => fetchDetails(p.value)} key={p.key}>
                {p.name}
              </li>
            ))}
          </ul>
          <div>
            <p>
              Numer konta:<b>{details?.accountNumber}</b>
            </p>
            <p>
              Saldo:{" "}
              <b>
                {details?.balance.toFixed(2)}
                <span> {details?.currency}</span>
              </b>
            </p>
            <button onClick={toggleTransfer}>Nowy przelew</button>
            {isTransferActive && <SendTransfer />}
          </div>
        </div>

        <div>Historia transakcji</div>
        <ul></ul>
      </div>
    </>
  );
}

export default Details;
