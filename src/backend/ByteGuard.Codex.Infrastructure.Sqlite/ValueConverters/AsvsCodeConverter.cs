using ByteGuard.Codex.Core.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ByteGuard.Codex.Infrastructure.Sqlite.ValueConverters;

internal class AsvsCodeConverter : ValueConverter<AsvsCode, string>
{
    public AsvsCodeConverter()
        : base(
            v => v.ToVersionString(),
            v => AsvsCode.Parse(v)
        )
    {
    }
}
