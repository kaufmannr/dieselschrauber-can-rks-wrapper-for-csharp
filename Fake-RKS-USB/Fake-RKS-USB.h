// RKS-USB.h : main header file for the RKS-USB DLL
//  User version
//

#pragma once
#include <windows.h>

// Configuration DLL (EXPORT_DLL) or simply included in EXE
#define EXPORT_DLL 1

// Needed software versions
#define RKS_NEEDED_BOOT_VERSION 1001
#define RKS_NEEDED_APP_VERSION  1060

// CAN frame types
#define FRAME_TYPE_NORMAL     0x1
#define FRAME_TYPE_RTR        0x2
#define FRAME_TYPE_NORMAL_EXT 0x3
#define FRAME_TYPE_RTR_EXT    0x4
#define FRAME_TYPE_ERR        0x5

// CAN data structure
typedef struct
{
	DWORD dwID;
	BYTE byDLC;
	BYTE abyData[8]; // max 8 data bytes
} can_data_t;

// CAN error information structure
typedef struct
{
	BYTE byError;
} can_err_t;

// CAN information structure
typedef union
{
	can_data_t sData;
	can_err_t sErr;
} can_union_t;

// The final CAN frame structure used for send/receive
typedef struct
{
	BYTE byType;
	DWORD dwTimeStamp;
	can_union_t uFrm;
} can_msg_t;

// DLL exports
#if(EXPORT_DLL)
// CRKSUSBApp
// See RKS-USB.cpp for the implementation of this class
//
//class CRKSUSBApp : public CWinApp
//{
//public:
//	CRKSUSBApp();
//
//	// Overrides
//public:
//	virtual BOOL InitInstance();
//
//	DECLARE_MESSAGE_MAP()
//};
#endif

#if(EXPORT_DLL)
#define FUNCTYPE extern "C" __declspec(dllexport)
#else
#define FUNCTYPE extern "C"
#endif

// Initialize USB driver, open connection to RKS+CAN hardware. Returns TRUE on success.
//  This command must be done once before calling any other RKS... function, except
//  RKSDeviceConnected.
FUNCTYPE BOOL RKSInitialize(void);

// Free USB driver and RKS+CAN hardware. Should be called to release interface/driver.
FUNCTYPE void RKSFree(void);

// Check if the RKS+CAN is connected to the computer. Returns TRUE on success. pcBufIfGUID can be NULL.
//  If pcBufIfGUID is not NULL, copy the DeviceInterfaceGUID of the RKS+CAN hardware to it.
//  If you check simply if the RKS+CAN is connected, it is recommended to call it with pcBufIfGUID = NULL.
FUNCTYPE BOOL RKSDeviceConnected(LPSTR pcBufIfGUID, DWORD dwBufSize);

// Set the timeouts for reading / writing data to the RKS+CAN interface. The software waits at maximum the 
//  specified values for read/write operations to complete. Returns TRUE on success.
FUNCTYPE BOOL RKSSetTimeouts(DWORD dwMsTimeoutRead, DWORD dwMsTimeoutWrite);

// Direct read from hardware USB pipes (low level). Not recommended for application use if you do not know what you do.
FUNCTYPE BOOL RKSReadPipe(PUCHAR pucBuffer, DWORD dwBufferLength, DWORD* pdwLengthTransferred, LPOVERLAPPED pOverlapped);

// Direct write to hardware USB pipes (low level). Not recommended for application use if you do not know what you do.
FUNCTYPE BOOL RKSWritePipe(PUCHAR pucBuffer, DWORD dwBufferLength, DWORD* pdwLengthTransferred, LPOVERLAPPED pOverlapped);

// Read from hardware using the specified timeout value as maximum time to finish the operation. Returns TRUE on success.
FUNCTYPE BOOL RKSRead(PUCHAR pucBuffer, DWORD dwBufferLength, DWORD* pdwLengthTransferred);

// Write to hardware using the specified timeout value as maximum time to finish the operation. Returns TRUE on success.
FUNCTYPE BOOL RKSWrite(PUCHAR pucBuffer, DWORD dwBufferLength, DWORD* pdwLengthTransferred);

// Get the version of the of the RKS+CAN hardware. Returns TRUE on success.
FUNCTYPE BOOL RKSGetVersion(PUCHAR pucBuffer, DWORD dwBufferLength);

// Get the serial number of the RKS+CAN hardware (printed on the cable case).  Returns TRUE on success.
FUNCTYPE BOOL RKSGetSerial(PUCHAR pucBuffer, DWORD dwBufferLength);

// Return time since RKS-USB driver initialisation. bReInit can be used to reset the time to zero.
//  Can be used to get relatively exact time information, the time is returned in seconds.
FUNCTYPE double RKSGetTimeSinceInit(BOOL bReInit = FALSE);

// Get the error status of the RKS+CAN interface in pbyStatus. Returns TRUE on success.
FUNCTYPE BOOL RKSCANGetLastStatus(BYTE* pbyStatus);

// Set RKS+CAN hardware to listen only mode. Returns TRUE on success.
FUNCTYPE BOOL RKSCANSetListenOnly(BOOL bEnabled);

// Set time stamp mode of the RKS+CAN hardware. E.g. 0, 1 or 2.
//  For possible values check the ASCII interface description. Returns TRUE on success.
FUNCTYPE BOOL RKSCANSetTimeStamp(BYTE byMode);

// Get time stamp mode. Returns TRUE on success.
FUNCTYPE BOOL RKSCANGetTimeStamp(BYTE*pbyMode);

// Get the supply voltage of the RKS+CAN hardware. Returns TRUE on success.
FUNCTYPE BOOL RKSCANGetUb(DWORD* pdwVoltage_mV);

// Set CAN hardware filtering of the RKS+CAN hardware. Returns TRUE on success.
FUNCTYPE BOOL RKSCANSetFilter(DWORD dwCode, DWORD dwMask);

// Open CAN bus with the desired bitrate. Returns TRUE on success.
FUNCTYPE BOOL RKSCANOpen(DWORD dwBitrate);

// Close CAN bus. Returns TRUE on success.
FUNCTYPE BOOL RKSCANClose(void);

// Get one CAN message from the receive queue, RKSCANOpen(...) must have
//  been called before. Returns TRUE on success.
FUNCTYPE BOOL RKSCANRx(can_msg_t* pMsg);

// Add one CAN message to the send queue, RKSCANOpen(...) must have
//  been called before. Returns TRUE on success.
FUNCTYPE BOOL RKSCANTx(can_msg_t* pMsg);