import { NavLink } from "react-router";
import navStyles from "../../../styles/ClientNavBar.module.css";

export interface ClientNavBarProps {
  isAdmin?: boolean;
}
const ClientNavBar: React.FC<ClientNavBarProps> = ({ isAdmin }) => {
  const deleteToken = () => {
    localStorage.removeItem("token");
  };
  return (
    <>
      <nav className={navStyles.navbar}>
        <div className={navStyles.navbarBrand}>
          <NavLink
            to="/bankAccounts"
            className={({ isActive }) =>
              isActive ? navStyles.navLinksActiveLogo : navStyles.navLinks
            }
          >
            PocketBank
          </NavLink>
        </div>
        <div>
          {!isAdmin && (
            <NavLink
              to="/accounts/profile"
              className={({ isActive }) =>
                isActive ? navStyles.navLinksActive : navStyles.navLinks
              }
            >
              Mój profil
            </NavLink>
          )}

          <NavLink
            to="/"
            className={`${navStyles.navLinks} ${navStyles.myButton}`}
            onClick={deleteToken}
          >
            Wyloguj
          </NavLink>
        </div>
      </nav>
    </>
  );
};
export default ClientNavBar;
