namespace MINIMART.Common.Enums
{
    public enum Gender
    {
        Male = 0,
        Female = 1,
        Other = 2
    }

    public enum Role
    {
        Admin = 0,
        Customer = 1
    }

    public enum UnitEnum
    {
        Box = 0,
        Pack = 1,
        Bottle = 2,
    }

    public enum VolumeEnum
    {
        Gam = 0,
        Kg = 1,
        Lit = 2,
        MlLit = 3
    }

    public enum OrderStatus
    {
        Approved = 0,
        Delivered = 1,
        Failed = 2,
        Cancled = 3,
        Done = 4,
    }

    public enum SortOrder
    {
        Ascending = 0,
        Descending = 1,
    }

    public enum AccountStatus
    {
        Active = 0,
        Inactive = 1,
    }
}
