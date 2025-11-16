using System;
using System.Runtime.InteropServices;

namespace AonDotNet
{
    internal static class AonNative
    {
        private const string LIB_NAME = "aon_core";

        // char* aon_json_to_aon(const char* json_c, const char* root_c);
        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr aon_json_to_aon(string json, string root);

        // char* aon_aon_to_json(const char* aon_c);
        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr aon_aon_to_json(string aon);

        // const char* aon_last_error();
        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr aon_last_error();

        // void aon_free_string(char* ptr);
        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void aon_free_string(IntPtr ptr);
    }
}
