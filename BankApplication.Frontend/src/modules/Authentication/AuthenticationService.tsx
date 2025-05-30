import axios from "axios";

export default class AuthenticationService {
  public static async login(model: LoginModel): Promise<string> {
    return (await axios.post<string>("accounts/login", model)).data;
  }

  public static async join(model: ClientForm): Promise<string> {
    return (await axios.post<string>("accounts/login", model)).data;
  }
}

export interface LoginModel {
  login: string;
  password: string;
  email: string;
}

export interface ClientForm {}
