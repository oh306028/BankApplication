import WelcomePage from "./WelcomePage";
import Login from "./modules/Authentication/views/Login";
import Register from "./modules/Authentication/views/Register";
import JoinAsClient from "./modules/Authentication/views/JoinAsClient";
import { Routes, Route } from "react-router";
import axios from "axios";
import BankAccounts from "./modules/Accounts/BankAccounts";
import AcceptationAwait from "./modules/Authentication/views/AcceptationAwait";

function App() {
  axios.defaults.baseURL = "https://localhost:7295/api";
  axios.defaults.headers.common["Content-Type"] = "application/json";

  return (
    <>
      <Routes>
        <Route path="/" element={<WelcomePage />} />
        <Route path="/accounts/login" element={<Login />} />
        <Route path="/accounts/register" element={<Register />} />
        <Route path="/accounts/join" element={<JoinAsClient />} />
        <Route path="/bankAccounts" element={<BankAccounts />} />
        <Route path="/accounts/acceptation" element={<AcceptationAwait />} />
      </Routes>
    </>
  );
}

export default App;
