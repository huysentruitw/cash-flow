import { Transaction } from './transaction';

export class TransactionWithBalance extends Transaction {
  balanceInCents!: number;
}
