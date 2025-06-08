import axios from "axios";

export default class AuthenticationService {
  public static async login(model: LoginModel): Promise<string> {
    return (await axios.post<string>("accounts/login", model)).data;
  }

  public static async join(model: ClientForm): Promise<string> {
    return (await axios.post<string>("clients", model)).data;
  }

  public static async register(model: RegisterModel): Promise<void> {
    await axios.post<void>("accounts/register", model);
  }
}

export interface LoginModel {
  login: string;
  password: string;
  email: string;
}

export interface ClientForm {
  publicId?: string;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  pesel: string;
  birthDate: Date | null;
  nationality: string;
  country: string;
  postalCode: string;
  number: string;
  city: string;
}

export interface RegisterModel extends LoginModel {
  confirmPassword: string;
  clientCode: string;
}
