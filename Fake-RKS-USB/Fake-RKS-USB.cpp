// This is the main DLL file.

#include "stdafx.h"

#include "Fake-RKS-USB.h"

typedef void(__stdcall * UniversalCallBack)(char* name, System::String^ value);

UniversalCallBack _universalCallBack;

extern "C" __declspec(dllexport) int SetUpModel(UniversalCallBack universalCallBack);

int SetUpModel(UniversalCallBack universalCallBack)
{
	_universalCallBack = universalCallBack;

	return 1;
}

// Initialize USB driver, open connection to RKS+CAN hardware. Returns TRUE on success.
//  This command must be done once before calling any other RKS... function, except
//  RKSDeviceConnected.
BOOL RKSInitialize(void) 
{
	_universalCallBack("RKSInitialize", gcnew System::String("void"));
	return true; 
}

// Free USB driver and RKS+CAN hardware. Should be called to release interface/driver.
 void RKSFree(void){}

// Check if the RKS+CAN is connected to the computer. Returns TRUE on success. pcBufIfGUID can be NULL.
//  If pcBufIfGUID is not NULL, copy the DeviceInterfaceGUID of the RKS+CAN hardware to it.
//  If you check simply if the RKS+CAN is connected, it is recommended to call it with pcBufIfGUID = NULL.
 BOOL RKSDeviceConnected(LPSTR pcBufIfGUID, DWORD dwBufSize){
	 System::String^ str = gcnew System::String(pcBufIfGUID);
	 _universalCallBack("RKSDeviceConnected", gcnew System::String(str + "," + dwBufSize));

	 // Write pcBufIfGUID
	 strcpy(pcBufIfGUID ,"1234");
	 dwBufSize = 4;

	 return true; }

// Set the timeouts for reading / writing data to the RKS+CAN interface. The software waits at maximum the 
//  specified values for read/write operations to complete. Returns TRUE on success.
 BOOL RKSSetTimeouts(DWORD dwMsTimeoutRead, DWORD dwMsTimeoutWrite){ 

	 _universalCallBack("RKSSetTimeouts", gcnew System::String(dwMsTimeoutRead + "," + dwMsTimeoutWrite));
	 return true; }

// Direct read from hardware USB pipes (low level). Not recommended for application use if you do not know what you do.
 BOOL RKSReadPipe(PUCHAR pucBuffer, DWORD dwBufferLength, DWORD* pdwLengthTransferred, LPOVERLAPPED pOverlapped){
	
	 _universalCallBack("RKSReadPipe", gcnew System::String(dwBufferLength + "," + *pdwLengthTransferred));

	 strcpy((char*)pucBuffer, "1234");
	 *pdwLengthTransferred = 4;

	 pOverlapped->Internal = 10;
	 pOverlapped->InternalHigh = 20;
	 pOverlapped->Offset = 30;
	 pOverlapped->OffsetHigh = 40;
	 
	 return true; }

// Direct write to hardware USB pipes (low level). Not recommended for application use if you do not know what you do.
 BOOL RKSWritePipe(PUCHAR pucBuffer, DWORD dwBufferLength, DWORD* pdwLengthTransferred, LPOVERLAPPED pOverlapped){
	 _universalCallBack("RKSWritePipe", 
		 gcnew System::String(
			 dwBufferLength + "," 
			 + *pdwLengthTransferred +","
			 + pOverlapped->Internal + ","
			 + pOverlapped->InternalHigh + ","
			 + pOverlapped->Offset + ","
			 + pOverlapped->OffsetHigh 
		 ));
	 	 
	 strcpy((char*)pucBuffer, "1234");
	 *pdwLengthTransferred = 4;

	 return true; }

// Read from hardware using the specified timeout value as maximum time to finish the operation. Returns TRUE on success.
 BOOL RKSRead(PUCHAR pucBuffer, DWORD dwBufferLength, DWORD* pdwLengthTransferred){
	 _universalCallBack("RKSRead", gcnew System::String(dwBufferLength + "," + *pdwLengthTransferred));

	 strcpy((char*)pucBuffer, "1234");
	 *pdwLengthTransferred = 4;

	 return true; }

// Write to hardware using the specified timeout value as maximum time to finish the operation. Returns TRUE on success.
 BOOL RKSWrite(PUCHAR pucBuffer, DWORD dwBufferLength, DWORD* pdwLengthTransferred){
	 char* text = new char[dwBufferLength];
	strcpy(text,(char*)pucBuffer);

	 _universalCallBack("RKSWrite", gcnew System::String(text) + gcnew System::String("," + dwBufferLength + "," + *pdwLengthTransferred));
	 
	 return true; }

// Get the version of the of the RKS+CAN hardware. Returns TRUE on success.
 BOOL RKSGetVersion(PUCHAR pucBuffer, DWORD dwBufferLength){
	 _universalCallBack("RKSGetVersion", gcnew System::String(dwBufferLength + ""));


	 strcpy((char*)pucBuffer, "1070");
	 return true; }

// Get the serial number of the RKS+CAN hardware (printed on the cable case).  Returns TRUE on success.
 BOOL RKSGetSerial(PUCHAR pucBuffer, DWORD dwBufferLength){
	 _universalCallBack("RKSGetSerial", gcnew System::String(dwBufferLength + ""));

	 strcpy((char*)pucBuffer, "{1-2-3-4}");
	 

	 return true; }

// Return time since RKS-USB driver initialisation. bReInit can be used to reset the time to zero.
//  Can be used to get relatively exact time information, the time is returned in seconds.
 double RKSGetTimeSinceInit(BOOL bReInit){
	 _universalCallBack("RKSGetTimeSinceInit", gcnew System::String(bReInit + ""));
	 return 1.0f; 
 }

// Get the error status of the RKS+CAN interface in pbyStatus. Returns TRUE on success.
 BOOL RKSCANGetLastStatus(BYTE* pbyStatus){
	 _universalCallBack("RKSCANGetLastStatus", gcnew System::String(*pbyStatus + ""));
	 *pbyStatus = 4;
	 return true; }

// Set RKS+CAN hardware to listen only mode. Returns TRUE on success.
 BOOL RKSCANSetListenOnly(BOOL bEnabled){
	 _universalCallBack("RKSCANSetListenOnly", gcnew System::String(bEnabled + ""));
	 return true; }

// Set time stamp mode of the RKS+CAN hardware. E.g. 0, 1 or 2.
//  For possible values check the ASCII interface description. Returns TRUE on success.
 BOOL RKSCANSetTimeStamp(BYTE byMode){
	 _universalCallBack("RKSCANSetTimeStamp", gcnew System::String(byMode + "")); return true; }

// Get time stamp mode. Returns TRUE on success.
 BOOL RKSCANGetTimeStamp(BYTE*pbyMode){
	 _universalCallBack("RKSCANGetTimeStamp", gcnew System::String(*pbyMode + ""));
	 
	 *pbyMode = 1;
	return true; }

// Get the supply voltage of the RKS+CAN hardware. Returns TRUE on success.
 BOOL RKSCANGetUb(DWORD* pdwVoltage_mV){
	 _universalCallBack("RKSCANGetUb", gcnew System::String(*pdwVoltage_mV + ""));
	 *pdwVoltage_mV = 12;
	return true; }

// Set CAN hardware filtering of the RKS+CAN hardware. Returns TRUE on success.
 BOOL RKSCANSetFilter(DWORD dwCode, DWORD dwMask){
	 _universalCallBack("RKSCANSetFilter", gcnew System::String(dwCode + "," + dwMask)); return true; }

// Open CAN bus with the desired bitrate. Returns TRUE on success.
 BOOL RKSCANOpen(DWORD dwBitrate){
	 _universalCallBack("RKSCANOpen", gcnew System::String(dwBitrate + "")); return true; }

// Close CAN bus. Returns TRUE on success.
 BOOL RKSCANClose(void){
	 _universalCallBack("RKSCANClose", gcnew System::String("void")); return true; }

// Get one CAN message from the receive queue, RKSCANOpen(...) must have
//  been called before. Returns TRUE on success.
 BOOL RKSCANRx(can_msg_t* pMsg){
	 _universalCallBack("RKSCANRx", gcnew System::String(pMsg->byType + "," + pMsg->dwTimeStamp +"," + pMsg->uFrm.sData.abyData[0]));  
	
	 pMsg->uFrm.sData.dwID = 10;
	 //pMsg->uFrm.sData.abyData = { 8,9 };
	 pMsg->uFrm.sData.abyData[0] = 90;
	 pMsg->uFrm.sData.abyData[1] =91;
	 pMsg->uFrm.sData.abyData[2] =92;
	 pMsg->uFrm.sData.abyData[3] =93;
	 pMsg->uFrm.sData.abyData[4] =94;
	 pMsg->uFrm.sData.abyData[5] =95;
	 pMsg->uFrm.sData.abyData[6] =96;
	 pMsg->uFrm.sData.abyData[7] =97;
	
	 pMsg->uFrm.sData.byDLC = 30;
	 pMsg->byType = 40;
	 pMsg->dwTimeStamp = 50;

	 //pMsg->uFrm.sErr.byError = 127; // exclusively with sData.dwId
	return true; }

// Add one CAN message to the send queue, RKSCANOpen(...) must have
//  been called before. Returns TRUE on success.
 BOOL RKSCANTx(can_msg_t* pMsg){
	 _universalCallBack("RKSCANTx", 
		 gcnew System::String(
			pMsg->byType + "," 
			+ pMsg->dwTimeStamp + ","
			+ pMsg->uFrm.sData.dwID+ "," 
			+ pMsg->uFrm.sData.byDLC+ ","
			+ pMsg->uFrm.sData.abyData[0]+ ","
			+ pMsg->uFrm.sData.abyData[1]+ ","
			+ pMsg->uFrm.sData.abyData[2]+ ","
			+ pMsg->uFrm.sData.abyData[3]+ ","
			+ pMsg->uFrm.sData.abyData[4]+ ","
			+ pMsg->uFrm.sData.abyData[5]+ ","
			+ pMsg->uFrm.sData.abyData[6]+ ","
			+ pMsg->uFrm.sData.abyData[7]
	 ));
	
	return true; }
