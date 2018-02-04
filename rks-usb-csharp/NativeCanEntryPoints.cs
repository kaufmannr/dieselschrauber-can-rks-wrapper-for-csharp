using System;
using System.Runtime.InteropServices;
using System.Text;

namespace rks_usb_csharp
{
    public class NativeCanEntryPoints
    {
        internal const string _RKSInitialize = "RKSInitialize";
        internal const string _RKSFree = "RKSFree";
        internal const string _RKSDeviceConnected = "RKSDeviceConnected";
        internal const string _RKSSetTimeouts = "RKSSetTimeouts";
        internal const string _RKSReadPipe = "RKSReadPipe";
        internal const string _RKSWritePipe = "RKSWritePipe";
        internal const string _RKSRead = "RKSRead";
        internal const string _RKSWrite = "RKSWrite";
        internal const string _RKSGetVersion = "RKSGetVersion";
        internal const string _RKSGetSerial = "RKSGetSerial";
        internal const string _RKSGetTimeSinceInit = "RKSGetTimeSinceInit";
        internal const string _RKSCANGetLastStatus = "RKSCANGetLastStatus";
        internal const string _RKSCANSetListenOnly = "RKSCANSetListenOnly";
        internal const string _RKSCANSetTimeStamp = "RKSCANSetTimeStamp";
        internal const string _RKSCANGetTimeStamp = "RKSCANGetTimeStamp";
        internal const string _RKSCANGetUb = "RKSCANGetUb";
        internal const string _RKSCANSetFilter = "RKSCANSetFilter";
        internal const string _RKSCANOpen = "RKSCANOpen";
        internal const string _RKSCANClose = "RKSCANClose";
        internal const string _RKSCANRx = "RKSCANRx";
        internal const string _RKSCANTx = "RKSCANTx";


        /// <summary>
        /// Initialize USB driver, open connection to RKS+CAN hardware. Returns TRUE on success.
        ///  This command must be done once before calling any other RKS... function, except
        ///  RKSDeviceConnected.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RKSInitialize();

        /// <summary>
        /// Free USB driver and RKS+CAN hardware. Should be called to release interface/driver.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RKSFree();


        /// <summary>
        /// Check if the RKS+CAN is connected to the computer. Returns TRUE on success. pcBufIfGUID can be NULL.
        ///  If pcBufIfGUID is not NULL, copy the DeviceInterfaceGUID of the RKS+CAN hardware to it.
        ///  If you check simply if the RKS+CAN is connected, it is recommended to call it with pcBufIfGUID = NULL.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RKSDeviceConnected(StringBuilder pcBufIfGUID, Int32 dwBufSize);

        /// <summary>
        /// Set the timeouts for reading / writing data to the RKS+CAN interface. The software waits at maximum the 
        ///  specified values for read/write operations to complete. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RKSSetTimeouts(Int32 dwMsTimeoutRead, Int32 dwMsTimeoutWrite);


        /// <summary>
        /// Direct read from hardware USB pipes (low level). Not recommended for application use if you do not know what you do.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RKSReadPipe(StringBuilder pucBuffer, Int32 dwBufferLength, ref int pdwLengthTransferred,
            ref LPOVERLAPPED pOverlapped);

        /// <summary>
        /// Direct write to hardware USB pipes (low level). Not recommended for application use if you do not know what you do.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RKSWritePipe(StringBuilder pucBuffer, Int32 dwBufferLength, ref int pdwLengthTransferred,
            ref LPOVERLAPPED pOverlapped);

        /// <summary>
        /// Read from hardware using the specified timeout value as maximum time to finish the operation. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RKSRead(StringBuilder pucBuffer, Int32 dwBufferLength, ref int pdwLengthTransferred);

        /// <summary>
        /// Write to hardware using the specified timeout value as maximum time to finish the operation. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RKSWrite(StringBuilder pucBuffer, Int32 dwBufferLength, ref int pdwLengthTransferred);

        /// Get the version of the of the RKS+CAN hardware. Returns TRUE on success.


        /// <summary>
        /// Get the version of the of the RKS+CAN hardware. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RKSGetVersion(
            StringBuilder version, Int32 dwSize);

        /// <summary>
        /// Get the serial number of the RKS+CAN hardware (printed on the cable case).  Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RKSGetSerial(
            StringBuilder serial, Int32 dwSize);


        /// <summary>
        /// Return time since RKS-USB driver initialisation. bReInit can be used to reset the time to zero.
        ///  Can be used to get relatively exact time information, the time is returned in seconds.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate double RKSGetTimeSinceInit(bool bReInit = false);

        /// <summary>
        /// Get the error status of the RKS+CAN interface in pbyStatus. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RKSCANGetLastStatus(ref byte pbyStatus);

        /// <summary>
        /// Set RKS+CAN hardware to listen only mode. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RKSCANSetListenOnly(bool bEnabled);

        /// <summary>
        /// Set time stamp mode of the RKS+CAN hardware. E.g. 0, 1 or 2.
        ///  For possible values check the ASCII interface description. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RKSCANSetTimeStamp(byte byMode);

        /// <summary>
        /// Get time stamp mode. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RKSCANGetTimeStamp(ref byte pbyMode);

        /// <summary>
        /// Get the supply voltage of the RKS+CAN hardware. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RKSCANGetUb(ref uint pdwVoltage_mV);

        /// <summary>
        /// Set CAN hardware filtering of the RKS+CAN hardware. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RKSCANSetFilter(Int32 dwCode, Int32 dwMask);

        /// <summary>
        /// Open CAN bus with the desired bitrate. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RKSCANOpen(Int32 dwBitrate);

        /// <summary>
        /// Close CAN bus. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RKSCANClose();

        /// <summary>
        /// Get one CAN message from the receive queue, RKSCANOpen(...) must have
        ///  been called before. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool RKSCANRx(ref can_msg_t pMsg);

        /// <summary>
        /// Add one CAN message to the send queue, RKSCANOpen(...) must have
        ///  been called before. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool RKSCANTx(ref can_msg_t pMsg);

        /// <summary>
        /// The final CAN frame structure used for send/receive
        /// </summary>
        /// <returns></returns>
        [StructLayout(LayoutKind.Explicit)]
        internal struct can_msg_t
        {
            [FieldOffset(0)] internal byte byType;
            [FieldOffset(4)] internal Int32 dwTimeStamp;
            [FieldOffset(8)] internal can_union_t uFrm;
        }

        /// <summary>
        /// CAN information structure
        /// </summary>
        /// <returns></returns>
        [StructLayout(LayoutKind.Explicit)]
        internal struct can_union_t
        {
            [FieldOffset(0)] internal can_data_t sData;
            [FieldOffset(0)] internal can_err_t sErr;
        }

        /// <summary>
        /// CAN data structure
        /// </summary>
        /// <returns></returns>
        [StructLayout(LayoutKind.Explicit)]
        internal struct can_data_t
        {
            [FieldOffset(0)] internal Int32 dwID;
            [FieldOffset(4)] internal byte byDLC;
            [FieldOffset(5)] internal byte abyData0; // max 8 data bytes
            [FieldOffset(6)] internal byte abyData1;
            [FieldOffset(7)] internal byte abyData2;
            [FieldOffset(8)] internal byte abyData3;
            [FieldOffset(9)] internal byte abyData4;
            [FieldOffset(10)] internal byte abyData5;
            [FieldOffset(11)] internal byte abyData6;
            [FieldOffset(12)] internal byte abyData7;
        }


        /// <summary>
        /// CAN error information structure
        /// </summary>
        /// <returns></returns>
        [StructLayout(LayoutKind.Explicit)]
        internal struct can_err_t
        {
            [FieldOffset(0)] internal byte byError;
        }

        [StructLayout(LayoutKind.Explicit, Size = 20)]
        public struct LPOVERLAPPED
        {
            [FieldOffset(0)]
            public uint Internal;

            [FieldOffset(4)]
            public uint InternalHigh;

            [FieldOffset(8)]
            public uint Offset;

            [FieldOffset(12)]
            public uint OffsetHigh;

            [FieldOffset(8)]
            public IntPtr Pointer;

            [FieldOffset(16)]
            public IntPtr hEvent;
        }
    }
}