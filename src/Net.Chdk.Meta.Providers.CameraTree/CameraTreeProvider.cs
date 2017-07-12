using Net.Chdk.Meta.Model.CameraTree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Net.Chdk.Meta.Providers.CameraTree
{
    sealed class CameraTreeProvider : ICameraTreeProvider
    {
        private IEnumerable<IInnerCameraTreeProvider> InnerProviders;

        public CameraTreeProvider(IEnumerable<IInnerCameraTreeProvider> innerProviders)
        {
            InnerProviders = innerProviders;
        }

        public IDictionary<string, TreePlatformData> GetCameraTree(string path)
        {
            var ext = Path.GetExtension(path);
            var writer = InnerProviders.SingleOrDefault(w => w.Extension.Equals(ext, StringComparison.OrdinalIgnoreCase));
            if (writer == null)
                throw new InvalidOperationException($"Unknown camera writer extension: {ext}");
            return writer.GetCameraTree(path);
        }
    }
}
