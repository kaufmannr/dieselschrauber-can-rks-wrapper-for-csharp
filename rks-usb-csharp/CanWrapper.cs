using System;
using System.Text;

namespace rks_usb_csharp
{
    public class CanWrapper : WrapperBase, IDisposable
    {
        public CanWrapper(string libraryPath, string dllName = "RKS-USB.dll") : base(libraryPath, dllName)
        {
        }


        /// <summary>
        /// Initialize USB driver, open connection to RKS+CAN hardware. Returns TRUE on success.
        ///  This command must be done once before calling any other RKS... function, except
        ///  RKSDeviceConnected.
        /// </summary>
        /// <returns></returns>
        public virtual void RKSInitialize()
        {
            Procedure<NativeCanEntryPoints.RKSInitialize>(NativeCanEntryPoints._RKSInitialize)();
        }

        /// <summary>
        /// Free USB driver and RKS+CAN hardware. Should be called to release interface/driver.
        /// </summary>
        /// <returns></returns>
        public virtual void RKSFree()
        {
            Procedure<NativeCanEntryPoints.RKSFree>(NativeCanEntryPoints._RKSFree)();
        }

        /// <summary>
        /// Check if the RKS+CAN is connected to the computer. Returns TRUE on success. pcBufIfGUID can be NULL.
        ///  If pcBufIfGUID is not NULL, copy the DeviceInterfaceGUID of the RKS+CAN hardware to it.
        ///  If you check simply if the RKS+CAN is connected, it is recommended to call it with pcBufIfGUID = NULL.
        /// </summary>
        /// <returns></returns>
        public virtual bool RKSDeviceConnected(out string guid)
        {
            const int capacity = 39;
            var sb = new StringBuilder(capacity);
            var deviceConnected =
                Procedure<NativeCanEntryPoints.RKSDeviceConnected>(NativeCanEntryPoints._RKSDeviceConnected)(sb,
                    capacity);

            guid = sb.ToString();
            return deviceConnected;
        }

        /// <summary>
        /// Set the timeouts for reading / writing data to the RKS+CAN interface. The software waits at maximum the 
        ///  specified values for read/write operations to complete. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        public virtual void RKSSetTimeouts(Int32 dwMsTimeoutRead, Int32 dwMsTimeoutWrite)
        {
            Procedure<NativeCanEntryPoints.RKSSetTimeouts>(NativeCanEntryPoints._RKSSetTimeouts)(
                dwMsTimeoutRead, dwMsTimeoutWrite);
        }

        /// <summary>
        /// Direct read from hardware USB pipes (low level). Not recommended for application use if you do not know what you do.
        /// </summary>
        /// <returns></returns>
        public virtual string RKSReadPipe(out NativeCanEntryPoints.LPOVERLAPPED pOverlapped)
        {
            const int capacity = 128;
            var sb = new StringBuilder(capacity);
            pOverlapped = new NativeCanEntryPoints.LPOVERLAPPED();
            int pdwLengthTransferred = new int();
            var result = Procedure<NativeCanEntryPoints.RKSReadPipe>(NativeCanEntryPoints._RKSReadPipe)(sb, capacity,
                ref pdwLengthTransferred, ref pOverlapped);

            if(!result) throw new Exception();

            return sb.ToString().Substring(0, pdwLengthTransferred);
        }

        /// <summary>
        /// Direct write to hardware USB pipes (low level). Not recommended for application use if you do not know what you do.
        /// </summary>
        /// <param name="overlapped"></param>
        /// <returns></returns>
        public virtual string RKSWritePipe(NativeCanEntryPoints.LPOVERLAPPED pOverlapped)
        {
            const int capacity = 128;
            var pucBuffer = new StringBuilder(capacity);
            var pdwLengthTransferred = new int();
            Procedure<NativeCanEntryPoints.RKSWritePipe>(NativeCanEntryPoints._RKSWritePipe)(pucBuffer, capacity,
                ref pdwLengthTransferred, ref pOverlapped);

            return pucBuffer.ToString();
        }

        /// <summary>
        /// Read from hardware using the specified timeout value as maximum time to finish the operation. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        public virtual string RKSRead()
        {
            const int capacity = 128;
            var sb = new StringBuilder(capacity);
            int pdwLengthTransferred = new int();
            Procedure<NativeCanEntryPoints.RKSRead>(NativeCanEntryPoints._RKSRead)(sb, capacity,
                ref pdwLengthTransferred);


            return sb.ToString().Substring(0, pdwLengthTransferred);
        }

        /// <summary>
        /// Write to hardware using the specified timeout value as maximum time to finish the operation. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        public virtual bool RKSWrite(string message)
        {
            const int capacity = 128;
            var sb = new StringBuilder(capacity);
            sb.Append(message);
            int pdwLengthTransferred = new int();
            return Procedure<NativeCanEntryPoints.RKSWrite>(NativeCanEntryPoints._RKSWrite)(sb, capacity,
                ref pdwLengthTransferred);
            
        }

        /// <summary>
        /// Get the version of the of the RKS+CAN hardware. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        public virtual string RKSGetVersion()
        {
            const int capacity = 32;
            var sb = new StringBuilder(capacity);
            Procedure<NativeCanEntryPoints.RKSGetVersion>(NativeCanEntryPoints._RKSGetVersion)(sb,
                capacity);

            return sb.ToString();
        }

        /// <summary>
        /// Get the serial number of the RKS+CAN hardware (printed on the cable case).  Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        public virtual string RKSGetSerial()
        {
            const int capacity = 32;
            var sb = new StringBuilder(capacity);
            Procedure<NativeCanEntryPoints.RKSGetSerial>(NativeCanEntryPoints._RKSGetSerial)(sb,
                capacity);

            return sb.ToString();
        }

        /// <summary>
        /// Return time since RKS-USB driver initialisation. bReInit can be used to reset the time to zero.
        ///  Can be used to get relatively exact time information, the time is returned in seconds.
        /// </summary>
        /// <returns></returns>
        public virtual double RKSGetTimeSinceInit(bool bReInit = false)
        {
            return Procedure<NativeCanEntryPoints.RKSGetTimeSinceInit>(NativeCanEntryPoints._RKSGetTimeSinceInit)(
                bReInit);
        }

        /// <summary>
        /// Get the error status of the RKS+CAN interface in pbyStatus. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        public virtual byte RKSCANGetLastStatus()
        {
            byte pbyStatus = new byte();
            var result = Procedure<NativeCanEntryPoints.RKSCANGetLastStatus>(NativeCanEntryPoints._RKSCANGetLastStatus)(
                ref pbyStatus);

            if (!result) throw new Exception();
            return pbyStatus;
        }

        /// <summary>
        /// Set RKS+CAN hardware to listen only mode. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        public virtual bool RKSCANSetListenOnly(bool bEnabled = false)
        {
            return Procedure<NativeCanEntryPoints.RKSCANSetListenOnly>(NativeCanEntryPoints._RKSCANSetListenOnly)(
                bEnabled);
        }

        /// <summary>
        /// Set time stamp mode of the RKS+CAN hardware. E.g. 0, 1 or 2.
        ///  For possible values check the ASCII interface description. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        public virtual bool RKSCANSetTimeStamp(byte byMode)
        {
            return Procedure<NativeCanEntryPoints.RKSCANSetTimeStamp>(NativeCanEntryPoints._RKSCANSetTimeStamp)(byMode);
        }

        /// <summary>
        /// Get time stamp mode. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        public virtual byte RKSCANGetTimeStamp()
        {
            var pbyMode = new byte();
            var result = Procedure<NativeCanEntryPoints.RKSCANGetTimeStamp>(NativeCanEntryPoints._RKSCANGetTimeStamp)(
                ref pbyMode);

            if (!result) throw new Exception();

            return pbyMode;
        }

        /// <summary>
        /// Get the supply voltage of the RKS+CAN hardware. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        public virtual uint RKSCANGetUb()
        {
            var pdwVoltage_mV = new uint();
            var result =
                Procedure<NativeCanEntryPoints.RKSCANGetUb>(NativeCanEntryPoints._RKSCANGetUb)(ref pdwVoltage_mV);
            if (!result) throw new Exception();
            return pdwVoltage_mV;
        }

        /// <summary>
        /// Set CAN hardware filtering of the RKS+CAN hardware. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        public virtual bool RKSCANSetFilter(Int32 dwCode, Int32 dwMask)
        {
            return Procedure<NativeCanEntryPoints.RKSCANSetFilter>(NativeCanEntryPoints._RKSCANSetFilter)(dwCode,
                dwMask);
        }

        /// <summary>
        /// Open CAN bus with the desired bitrate. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        public virtual bool RKSCANOpen(Int32 dwBitrate)
        {
            return Procedure<NativeCanEntryPoints.RKSCANOpen>(NativeCanEntryPoints._RKSCANOpen)(dwBitrate);
        }

        /// <summary>
        /// Close CAN bus. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        public virtual bool RKSCANClose()
        {
            return Procedure<NativeCanEntryPoints.RKSCANClose>(NativeCanEntryPoints._RKSCANClose)();
        }

        /// <summary>
        /// Get one CAN message from the receive queue, RKSCANOpen(...) must have
        ///  been called before. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        public virtual CanMessage RKSCANRx()
        {
            NativeCanEntryPoints.can_msg_t pMsg = new NativeCanEntryPoints.can_msg_t();
            var result = Procedure<NativeCanEntryPoints.RKSCANRx>(NativeCanEntryPoints._RKSCANRx)(ref pMsg);

            if (!result) throw new Exception();

            var canMessage = new CanMessage
            {
                byType = pMsg.byType,
                dwTimeStamp = pMsg.dwTimeStamp,
                byError = pMsg.uFrm.sErr.byError,
                dwID = pMsg.uFrm.sData.dwID,
                byDLC = pMsg.uFrm.sData.byDLC,
                abyData = new byte[]
                {
                    pMsg.uFrm.sData.abyData0,
                    pMsg.uFrm.sData.abyData1,
                    pMsg.uFrm.sData.abyData2,
                    pMsg.uFrm.sData.abyData3,
                    pMsg.uFrm.sData.abyData4,
                    pMsg.uFrm.sData.abyData5,
                    pMsg.uFrm.sData.abyData6,
                    pMsg.uFrm.sData.abyData7,
                }
            };

            return canMessage;
        }

        /// <summary>
        /// Add one CAN message to the send queue, RKSCANOpen(...) must have
        ///  been called before. Returns TRUE on success.
        /// </summary>
        /// <returns></returns>
        public virtual bool RKSCANTx(CanMessage canMessage)
        {
            NativeCanEntryPoints.can_msg_t pMsg = new NativeCanEntryPoints.can_msg_t();

            pMsg.byType = canMessage.byType;
            pMsg.dwTimeStamp = canMessage.dwTimeStamp;
            pMsg.uFrm.sData.dwID = canMessage.dwID;
            pMsg.uFrm.sData.byDLC = canMessage.byDLC;
            pMsg.uFrm.sData.abyData0 = canMessage.abyData[0];
            pMsg.uFrm.sData.abyData1 = canMessage.abyData[1];
            pMsg.uFrm.sData.abyData2 = canMessage.abyData[2];
            pMsg.uFrm.sData.abyData3 = canMessage.abyData[3];
            pMsg.uFrm.sData.abyData4 = canMessage.abyData[4];
            pMsg.uFrm.sData.abyData5 = canMessage.abyData[5];
            pMsg.uFrm.sData.abyData6 = canMessage.abyData[6];
            pMsg.uFrm.sData.abyData7 = canMessage.abyData[7];

            return Procedure<NativeCanEntryPoints.RKSCANTx>(NativeCanEntryPoints._RKSCANTx)(ref pMsg);
        }

        /// <summary>
        /// The final CAN frame structure used for send/receive
        /// </summary>
        /// <returns></returns>
        public class CanMessage
        {
            public byte byType;
            public Int32 dwTimeStamp;

            public byte byError;

            // only filles on Success
            public Int32 dwID;
            public byte byDLC;
            public byte[] abyData; // max 8 data bytes
        }
    }
}