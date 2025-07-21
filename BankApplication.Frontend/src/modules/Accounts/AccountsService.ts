import axios from "axios";
export default class AccountsService {
  public static async hasBankAccounts(): Promise<boolean> {
    return (await axios.get<boolean>(`bank-accounts`)).data;
  }

  public static async getTypes(): Promise<KeyValueParir[]> {
    return (await axios.get<KeyValueParir[]>(`bank-accounts/types`)).data;
  }
}

export interface KeyValueParir {
  key: number;
  value: string;
  name: string;
}
