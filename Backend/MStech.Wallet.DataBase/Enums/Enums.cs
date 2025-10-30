using System.ComponentModel;

namespace MStech.Accounting.DataBase.Enums;

public enum OutcomeType
{
    [Description("درآمد")] NetIncome = 1,
    [Description("هزینه")] NetLoss = 2,
}

public enum PayerType
{
    [Description("مالک")] Owner = 1,
    [Description("مستاجر")] Resident = 2,
}

public enum NotifSubjectType
{
    [Description("پرداخت شارژ")] OutcomePayment = 1,
}

public enum PaymentStatus
{
    [Description("پرداخت شده ")] Paid = 1,

    [Description("پرداخت نشده ")] NotPaid = 2,
    [Description("پرداخت معلق ")] Onhold = 3
}

public enum PaymentMethod
{
    [Description("نقدی")] cash = 1,
    [Description("درگاه")] Online = 2,
    [Description("کارت به کارت")] AccountTransaction = 3,
    [Description("ندارد")] None = 4
}

public enum BankPaymentStatusCode
{
    Payed = 1,
    NotPayed = 2,
    Onhold = 3
}

public enum WalletIssueType
{
    [Description("افزایش")] Increase = 1,
    [Description("کاهش")] Decrease = 2,
}

public enum OutcomeDetailType
{
    [Description("شارژ")] Charge = 1,
    [Description("آب بها")] Water = 2,
}

public enum BillStatus
{
    [Description("پرداخت شده")] Paid = 1,
    [Description("پرداخت نشده")] NotPaid = 2,
    [Description("پرداخت معلق")] Onhold = 3
}

public enum ClientStatus
{
    [Description("فعال")] Active = 1,
    [Description("غیرفعال")] Deactive = 2,
    [Description("معلق")] Onhold = 3
}

public enum WalletStatus
{
    [Description("فعال")] Active = 1,
    [Description("غیرفعال")] Deactive = 2,
    [Description("معلق")] Onhold = 3
}

public enum WalletType
{
    [Description("کمیسیون")] Commission = 1,
    [Description("خرید")] Purchase = 2,
    [Description("پدر")] Parent = 3,
    [Description("صاحب پروژه")] ClientOwner = 4,
}

public enum TransactionType
{
    [Description("پرداخت درگاه")] Purches = 1,
    [Description("صدور فاکتور")] IssuanceOfAnInvoice = 6,
    [Description("شارژ مستقیم")] Charge = 2,
    [Description("هدیه ")] Gift = 3,
    [Description("سهم مجری")] Commission = 4,
    [Description("استرداد")] Refund = 5
}

public enum WalletTransactionCalculationType
{
    [Description("واریز")] Deposit = 1,
    [Description("برداشت")] Withdraw = 2
}

public enum DiscountCodeBankSpendType
{
    [Description("به ازای هر نفر")] PerUser = 1,
    [Description("به ازاری فاکتور")] PerInvoice = 2
}