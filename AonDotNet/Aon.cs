using System;
using System.Runtime.InteropServices;

namespace AonDotNet
{
    public static class Aon
    {
        private static string PtrToStringAndFree(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return string.Empty;

            try
            {
                // Assume UTF-8 encoded null-terminated string
                // Marshal.PtrToStringAnsi works for basic ASCII/UTF-8-compatible bytes
                return Marshal.PtrToStringAnsi(ptr) ?? string.Empty;
            }
            finally
            {
                AonNative.aon_free_string(ptr);
            }
        }

        private static string? GetLastError()
        {
            IntPtr errPtr = AonNative.aon_last_error();
            if (errPtr == IntPtr.Zero)
                return null;

            // Aqui não chamamos free explicitamente, assumindo que o lado Rust gerencia ou que o erro é pequeno.
            // Se necessário, poderíamos expor aon_free_string para isso também.
            return Marshal.PtrToStringAnsi(errPtr);
        }

        public static string JsonToAon(string json, string root)
        {
            if (json is null) throw new ArgumentNullException(nameof(json));
            if (root is null) throw new ArgumentNullException(nameof(root));

            IntPtr ptr = AonNative.aon_json_to_aon(json, root);
            if (ptr == IntPtr.Zero)
            {
                var err = GetLastError();
                throw new InvalidOperationException($"AON error: {err}");
            }

            return PtrToStringAndFree(ptr);
        }

        public static string AonToJson(string aon)
        {
            if (aon is null) throw new ArgumentNullException(nameof(aon));

            IntPtr ptr = AonNative.aon_aon_to_json(aon);
            if (ptr == IntPtr.Zero)
            {
                var err = GetLastError();
                throw new InvalidOperationException($"AON error: {err}");
            }

            return PtrToStringAndFree(ptr);
        }
    }
}
