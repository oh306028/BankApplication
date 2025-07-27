import { NavLink } from "react-router";
import navStyles from "../../../styles/ClientNavBar.module.css";

function ClientNavBar() {
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
          <NavLink
            to="/accounts/details"
            className={({ isActive }) =>
              isActive ? navStyles.navLinksActive : navStyles.navLinks
            }
          >
            Mój profil
          </NavLink>

          <button className={`${navStyles.navLinks} ${navStyles.myButton}`}>
            Wyloguj
          </button>
        </div>
      </nav>
    </>
  );
}
export default ClientNavBar;
