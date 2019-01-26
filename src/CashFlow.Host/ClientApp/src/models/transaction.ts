export class Transaction {
  id!: string;
  evidenceNumber!: number;
  financialYearId!: string;
  accountId!: string;
  supplierId: string;
  dateCreated!: Date;
  dateModified: Date;
  amountInCents!: number;
  isInternalTransfer!: boolean;
  description!: string;
  comment: string;
}
