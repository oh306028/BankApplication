import Footer from "../../../Footer";
import ClientNavBar from "../ClientAccount/ClientNavBar";
import AllTransferList from "./AllTransferList";
import BankAccountList from "./BankAccountList";
import ClientsList from "./ClientsList";

function AdminAccounts() {
  return (
    <>
      <ClientNavBar isAdmin={true} />

      <div>
        <ul>
          <li>
            <p>Administratorzy</p>
          </li>
          <li>
            <p>Klienci banku</p>
          </li>
          <li>
            <p>Konta klientów</p>
          </li>
          <li>
            <p>Prośby o zablokowanie konta</p>
          </li>
          <li>
            <p>Transfery</p>
          </li>
          <li>
            <p>Logowania</p>
          </li>
          <li>
            <p>Wnioski o zostanie klientem</p>
          </li>
        </ul>
      </div>
      <div>
        <ClientsList />
        <BankAccountList />
        <AllTransferList />
      </div>

      <Footer />
    </>
  );
}

export default AdminAccounts;
