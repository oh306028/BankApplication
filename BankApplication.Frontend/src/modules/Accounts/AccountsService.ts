import axios from "axios";
export default class AccountsService {
  public static async hasBankAccounts(): Promise<boolean> {
    return (await axios.get<boolean>(`bank-accounts`)).data;
  }

  public static async getTypes(): Promise<KeyValuePair[]> {
    return (await axios.get<KeyValuePair[]>(`bank-accounts/types`)).data;
  }

  public static async manageBlockRequest(
    model: BlockRequestModel,
    accountId: string
  ): Promise<void> {
    await axios.post<BlockRequestModel>(
      `bank-accounts/${accountId}/manage-block-request`,
      model
    );
  }

  public static async getOwnTypes(): Promise<KeyValuePair[]> {
    return (await axios.get<KeyValuePair[]>(`bank-accounts/own-types`)).data;
  }

  public static async getDetailsByType(type: string): Promise<Details> {
    return (await axios.get<Details>(`bank-accounts/${type}`)).data;
  }

  public static async getInterestRates(): Promise<KeyValuePair[]> {
    return (await axios.get<KeyValuePair[]>(`bank-accounts/rates`)).data;
  }

  public static async getCurrencies(): Promise<KeyValuePair[]> {
    return (await axios.get<KeyValuePair[]>(`bank-accounts/currencies`)).data;
  }

  public static async getCreditAmounts(): Promise<KeyValuePair[]> {
    return (await axios.get<KeyValuePair[]>(`bank-accounts/credits`)).data;
  }

  public static async hasBLockRequests(accountId: string): Promise<boolean> {
    return (
      await axios.get<boolean>(
        `bank-accounts/${accountId}/has-active-block-request`
      )
    ).data;
  }

  public static async isBlocked(accountId: string): Promise<boolean> {
    return (await axios.get<boolean>(`bank-accounts/${accountId}/isBlocked`))
      .data;
  }

  public static async createBankAccount(model: Form): Promise<void> {
    await axios.post<KeyValuePair[]>(`bank-accounts`, model);
  }

  public static async sendBlockRequest(accountId: string): Promise<void> {
    await axios.post(`bank-accounts/${accountId}/block-request`);
  }

  public static async getClientList(): Promise<ClientDetails[]> {
    return (await axios.get<ClientDetails[]>(`clients/dictionary/list`)).data;
  }

  public static async getDetails(): Promise<ProfileDetails> {
    return (await axios.get<ProfileDetails>(`accounts/profile`)).data;
  }

  public static async getAdminList(): Promise<ClientDetails[]> {
    return (await axios.get<ClientDetails[]>(`accounts/dictionary/admins`))
      .data;
  }

  public static async getBankAccounts(): Promise<Details[]> {
    return (await axios.get<Details[]>(`bank-accounts/dictionary/list`)).data;
  }
  public static async getBlockRequests(): Promise<BlockRequestDetails[]> {
    return (
      await axios.get<BlockRequestDetails[]>(
        `clients/dictionary/block-requests`
      )
    ).data;
  }

  public static async getLoginAttempts(): Promise<LoginAttemptDetails[]> {
    return (
      await axios.get<LoginAttemptDetails[]>(
        `clients/dictionary/login-attempts`
      )
    ).data;
  }
}

export interface ProfileDetails {
  login: string;
  isDoubleAuthenticated: boolean;
  createdDate: string;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  pesel: string;
  birthDate: string;
  country: string;
  city: string;
  postalCode: string;
  number: string;
}

export interface BlockRequestDetails {
  requestDate: string;
  managedDate: string;
  isAccepted?: boolean;
  isActive?: boolean;
  clientName: string;
  bankAccountNumber: string;
  publicId: string;
  accountId: string;
}

export interface BlockRequestModel {
  publicId: string;
  accepted: boolean;
}

export interface LoginAttemptDetails {
  logInDate: string;
  isSuccess: boolean;
  clientName: string;
}

export interface KeyValuePair {
  key: number;
  value: string;
  name: string;
}

export interface Form {
  type: number | string | KeyValuePair;
  currency: string;
  interestRate?: number;
  credit?: number;
}

export interface Details {
  accountNumber: string;
  balance: number;
  currency: string;
  interestRate?: number;
  publicId: string;
}

export interface ClientDetails {
  fullName: string;
  email: string;
  phone: string;
  nationality: string;
  country: string;
  city: string;
  postalCode: string;
  number: string;
  pesel: string;
}
