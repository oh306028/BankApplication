import { useState } from "react";
import Footer from "../../../Footer";
import ClientNavBar from "../ClientAccount/ClientNavBar";
import AdminList from "./AdminList";
import AllTransferList from "./AllTransferList";
import BankAccountList from "./BankAccountList";
import ClientsList from "./ClientsList";
import styles from "../../../styles/AdminAccounts.module.css";
import LoginAttempts from "./LoginAttempts";
import BlockadeRequests from "./BlockadeRequests";

type AdminView =
  | "admins"
  | "clients"
  | "accounts"
  | "blockRequests"
  | "transfers"
  | "logins"
  | "applications";

function AdminAccounts() {
  const [activeView, setActiveView] = useState<AdminView>("admins");

  const menuItems = [
    { key: "admins", label: "Administratorzy" },
    { key: "clients", label: "Klienci banku" },
    { key: "accounts", label: "Konta klientów" },
    { key: "blockRequests", label: "Wnioski o zablokowanie konta" },
    { key: "transfers", label: "Transfery" },
    { key: "logins", label: "Logowania" },
    { key: "applications", label: "Wnioski o zostanie klientem" },
  ];

  const renderContent = () => {
    switch (activeView) {
      case "admins":
        return <AdminList />;
      case "clients":
        return <ClientsList />;
      case "accounts":
        return <BankAccountList />;
      case "transfers":
        return <AllTransferList />;
      case "logins":
        return <LoginAttempts />;
      case "blockRequests":
        return <BlockadeRequests />;
      case "applications":
        return <p className={styles.placeholder}>Ta sekcja jest w budowie.</p>;
      default:
        return (
          <p className={styles.placeholder}>
            Wybierz opcję z menu, aby wyświetlić dane.
          </p>
        );
    }
  };

  return (
    <>
      <ClientNavBar isAdmin={true} />
      <div className={styles.adminPage}>
        <nav className={styles.sideMenu}>
          <ul className={styles.menuList}>
            {menuItems.map((item) => (
              <li
                key={item.key}
                className={
                  activeView === item.key ? styles.activeItem : styles.menuItem
                }
                onClick={() => setActiveView(item.key as AdminView)}
              >
                {item.label}
              </li>
            ))}
          </ul>
        </nav>

        <main className={styles.contentArea}>{renderContent()}</main>
      </div>
      <Footer />
    </>
  );
}

export default AdminAccounts;
