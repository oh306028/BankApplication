import WelcomePage from "./WelcomePage";
import Login from "./modules/Authentication/views/Login";
import Register from "./modules/Authentication/views/Register";
import JoinAsClient from "./modules/Authentication/views/JoinAsClient";
import { Routes, Route, useNavigate } from "react-router";
import axios from "axios";
import BankAccounts from "./modules/Accounts/BankAccounts";
import AcceptationAwait from "./modules/Authentication/views/AcceptationAwait";

function App() {
  const navigate = useNavigate();
  axios.defaults.baseURL = "https://localhost:7295/api";
  axios.defaults.headers.common["Content-Type"] = "application/json";

  axios.interceptors.request.use(
    (config) => {
      const token = localStorage.getItem("token");
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    },
    (error) => Promise.reject(error)
  );

  axios.interceptors.response.use(
    (response) => response,
    (error) => {
      if (error.response?.status === 401) {
        localStorage.removeItem("token");
        navigate("/accounts/login");
      }
      return Promise.reject(error);
    }
  );
  [navigate];

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
