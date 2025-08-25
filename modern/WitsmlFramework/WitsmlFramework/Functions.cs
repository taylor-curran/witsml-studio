using System.ComponentModel;

namespace WitsmlFramework;

public enum Functions
{
    [Description("Get Base Message")]
    GetBaseMsg,

    [Description("Get Capabilities")]
    GetCap,

    [Description("Get Version")]
    GetVersion,

    [Description("Get From Store")]
    GetFromStore,

    [Description("Add To Store")]
    AddToStore,

    [Description("Update In Store")]
    UpdateInStore,

    [Description("Delete From Store")]
    DeleteFromStore
}
