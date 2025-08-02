import axios from "axios";
export default class TransferService {
  public static async send(accountId: string, model: Form): Promise<void> {
    await axios.post<Form>(`bank-accounts/${accountId}/transfers`, model);
  }

  public static async getList(accountId: string): Promise<Details[]> {
    return (await axios.get<Details[]>(`bank-accounts/${accountId}/transfers`))
      .data;
  }
}

export interface Form {
  accountToNumber: string;
  amount: number;
  title: string;
  description: string;
}

export interface Details {
  senderNumber: string;
  receiverNumber: string;
  sender: string;
  receiver: string;
  amount: number;
  title: string;
  descrition: string;
  transferDate: string;
}
