namespace DigilentWaveForms
{
    public class Base
    {
        // device enumeration filters
        public const int enumfilterAll = 0;
        public const int enumfilterEExplorer = 1;
        public const int enumfilterDiscovery = 2;

        // device ID
        public const int devidEExplorer = 1;
        public const int devidDiscovery = 2;
        public const int devidDiscovery2 = 3;
        public const int devidDDiscovery = 4;

        // device version
        public const int devverEExplorerC = 2;
        public const int devverEExplorerE = 4;
        public const int devverEExplorerF = 5;
        public const int devverDiscoveryA = 1;
        public const int devverDiscoveryB = 2;
        public const int devverDiscoveryC = 3;

        // trigger source
        public const byte trigsrcNone = 0;
        public const byte trigsrcPC = 1;
        public const byte trigsrcDetectorAnalogIn = 2;
        public const byte trigsrcDetectorDigitalIn = 3;
        public const byte trigsrcAnalogIn = 4;
        public const byte trigsrcDigitalIn = 5;
        public const byte trigsrcDigitalOut = 6;
        public const byte trigsrcAnalogOut1 = 7;
        public const byte trigsrcAnalogOut2 = 8;
        public const byte trigsrcAnalogOut3 = 9;
        public const byte trigsrcAnalogOut4 = 10;
        public const byte trigsrcExternal1 = 11;
        public const byte trigsrcExternal2 = 12;
        public const byte trigsrcExternal3 = 13;
        public const byte trigsrcExternal4 = 14;

        // instrument states:
        public const byte DwfStateReady = 0;
        public const byte DwfStateConfig = 4;
        public const byte DwfStatePrefill = 5;
        public const byte DwfStateArmed = 1;
        public const byte DwfStateWait = 7;
        public const byte DwfStateTriggered = 3;
        public const byte DwfStateRunning = 3;
        public const byte DwfStateDone = 2;

        // acquisition modes:
        public const int acqmodeSingle = 0;
        public const int acqmodeScanShift = 1;
        public const int acqmodeScanScreen = 2;
        public const int acqmodeRecord = 3;
        public const int acqmodeOvers = 4;
        public const int acqmodeSingle1 = 5;

        // analog acquisition filter:
        public const int filterDecimate = 0;
        public const int filterAverage = 1;
        public const int filterMinMax = 2;

        // analog acquisition filter:
        public const int DwfTriggerSlopeRise = 0;
        public const int DwfTriggerSlopeFall = 1;
        public const int DwfTriggerSlopeEdge = 2;


        // analog input coupling:
        public const int DwfAnalogCouplingDC = 0;
        public const int DwfAnalogCouplingAC = 1;


        // analog in trigger mode:
        public const int trigtypeEdge = 0;
        public const int trigtypePulse = 1;
        public const int trigtypeTransition = 2;


        // analog in trigger length condition
        public const int triglenLess = 0;
        public const int triglenTimeout = 1;
        public const int triglenMore = 2;

        // error codes for DWF Public API:                         
        public const int dwfercNoErc = 0;        //  No error occurred
        public const int dwfercUnknownError = 1;        //  API waiting on pending API timed out
        public const int dwfercApiLockTimeout = 2;        //  API waiting on pending API timed out
        public const int dwfercAlreadyOpened = 3;        //  Device already opened
        public const int dwfercNotSupported = 4;        //  Device not supported
        public const int dwfercInvalidParameter0 = 0x10;     //  Invalid parameter sent in API call
        public const int dwfercInvalidParameter1 = 0x11;     //  Invalid parameter sent in API call
        public const int dwfercInvalidParameter2 = 0x12;     //  Invalid parameter sent in API call
        public const int dwfercInvalidParameter3 = 0x13;     //  Invalid parameter sent in API call
        public const int dwfercInvalidParameter4 = 0x14;     //  Invalid parameter sent in API call

        // analog out signal types
        public const byte funcDC = 0;
        public const byte funcSine = 1;
        public const byte funcSquare = 2;
        public const byte funcTriangle = 3;
        public const byte funcRampUp = 4;
        public const byte funcRampDown = 5;
        public const byte funcNoise = 6;
        public const byte funcPulse = 7;
        public const byte funcTrapezium = 8;
        public const byte funcSinePower = 9;
        public const byte funcCustom = 30;
        public const byte funcPlay = 31;

        // analog io channel node types
        public const byte analogioEnable = 1;
        public const byte analogioVoltage = 2;
        public const byte analogioCurrent = 3;
        public const byte analogioPower = 4;
        public const byte analogioTemperature = 5;
        public const byte analogioDmm = 6;
        public const byte analogioRange = 7;
        public const byte analogioMeasure = 8;

        public const int AnalogOutNodeCarrier = 0;
        public const int AnalogOutNodeFM = 1;
        public const int AnalogOutNodeAM = 2;

        public const int DwfAnalogOutModeVoltage = 0;
        public const int DwfAnalogOutModeCurrent = 1;

        public const int DwfAnalogOutIdleDisable = 0;
        public const int DwfAnalogOutIdleOffset = 1;
        public const int DwfAnalogOutIdleInitial = 2;

        public const int DwfDigitalInClockSourceInternal = 0;
        public const int DwfDigitalInClockSourceExternal = 1;

        public const int DwfDigitalInSampleModeSimple = 0;
        // alternate samples: noise|sample|noise|sample|...  
        // where noise is more than 1 transition between 2 samples
        public const int DwfDigitalInSampleModeNoise = 1;

        public const int DwfDigitalOutOutputPushPull = 0;
        public const int DwfDigitalOutOutputOpenDrain = 1;
        public const int DwfDigitalOutOutputOpenSource = 2;
        public const int DwfDigitalOutOutputThreeState = 3; // for custom and random

        public const int DwfDigitalOutTypePulse = 0;
        public const int DwfDigitalOutTypeCustom = 1;
        public const int DwfDigitalOutTypeRandom = 2;
        public const int DwfDigitalOutTypeFSM = 3;

        public const int DwfDigitalOutIdleInit = 0;
        public const int DwfDigitalOutIdleLow = 1;
        public const int DwfDigitalOutIdleHigh = 2;
        public const int DwfDigitalOutIdleZet = 3;


        public static int FDwfGetLastError(ref int pdwferc)
        {
            int ret = PINVOKE.FDwfGetLastError(ref pdwferc);
            return ret;
        }

        public static int FDwfGetLastErrorMsg(string szError)
        {
            int ret = PINVOKE.FDwfGetLastErrorMsg(szError);
            return ret;
        }

        public static int FDwfGetVersion(string szVersion)
        {
            int ret = PINVOKE.FDwfGetVersion(szVersion);
            return ret;
        }

        public static int FDwfEnum(int enumfilter, ref int pcDevice)
        {
            int ret = PINVOKE.FDwfEnum(enumfilter, ref pcDevice);
            return ret;
        }

        public static int FDwfEnumDeviceType(int idxDevice, ref int pDeviceId, ref int pDeviceRevision)
        {
            int ret = PINVOKE.FDwfEnumDeviceType(idxDevice, ref pDeviceId, ref pDeviceRevision);
            return ret;
        }

        public static int FDwfEnumDeviceIsOpened(int idxDevice, ref int pfIsUsed)
        {
            int ret = PINVOKE.FDwfEnumDeviceIsOpened(idxDevice, ref pfIsUsed);
            return ret;
        }

        public static int FDwfEnumUserName(int idxDevice, string szUserName)
        {
            int ret = PINVOKE.FDwfEnumUserName(idxDevice, szUserName);
            return ret;
        }

        public static int FDwfEnumDeviceName(int idxDevice, string szDeviceName)
        {
            int ret = PINVOKE.FDwfEnumDeviceName(idxDevice, szDeviceName);
            return ret;
        }

        public static int FDwfEnumSN(int idxDevice, string szSN)
        {
            int ret = PINVOKE.FDwfEnumSN(idxDevice, szSN);
            return ret;
        }

        public static int FDwfEnumConfig(int idxDevice, ref int pcConfig)
        {
            int ret = PINVOKE.FDwfEnumConfig(idxDevice, ref pcConfig);
            return ret;
        }

        public static int FDwfEnumConfigInfo(int idxConfig, int info, ref int pv)
        {
            int ret = PINVOKE.FDwfEnumConfigInfo(idxConfig, info, ref pv);
            return ret;
        }

        public static int FDwfDeviceOpen(int idxDevice, ref int phdwf)
        {
            int ret = PINVOKE.FDwfDeviceOpen(idxDevice, ref phdwf);
            return ret;
        }

        public static int FDwfDeviceConfigOpen(int idxDev, int idxCfg, ref int phdwf)
        {
            int ret = PINVOKE.FDwfDeviceConfigOpen(idxDev, idxCfg, ref phdwf);
            return ret;
        }

        public static int FDwfDeviceClose(int hdwf)
        {
            int ret = PINVOKE.FDwfDeviceClose(hdwf);
            return ret;
        }

        public static int FDwfDeviceCloseAll()
        {
            int ret = PINVOKE.FDwfDeviceCloseAll();
            return ret;
        }

        public static int FDwfDeviceAutoConfigureSet(int hdwf, int fAutoConfigure)
        {
            int ret = PINVOKE.FDwfDeviceAutoConfigureSet(hdwf, fAutoConfigure);
            return ret;
        }

        public static int FDwfDeviceAutoConfigureGet(int hdwf, ref int pfAutoConfigure)
        {
            int ret = PINVOKE.FDwfDeviceAutoConfigureGet(hdwf, ref pfAutoConfigure);
            return ret;
        }

        public static int FDwfDeviceReset(int hdwf)
        {
            int ret = PINVOKE.FDwfDeviceReset(hdwf);
            return ret;
        }

        public static int FDwfDeviceEnableSet(int hdwf, int fEnable)
        {
            int ret = PINVOKE.FDwfDeviceEnableSet(hdwf, fEnable);
            return ret;
        }

        public static int FDwfDeviceTriggerInfo(int hdwf, ref int pfstrigsrc)
        {
            int ret = PINVOKE.FDwfDeviceTriggerInfo(hdwf, ref pfstrigsrc);
            return ret;
        }

        public static int FDwfDeviceTriggerSet(int hdwf, int idxPin, byte trigsrc)
        {
            int ret = PINVOKE.FDwfDeviceTriggerSet(hdwf, idxPin, trigsrc);
            return ret;
        }

        public static int FDwfDeviceTriggerGet(int hdwf, int idxPin, ref byte ptrigsrc)
        {
            int ret = PINVOKE.FDwfDeviceTriggerGet(hdwf, idxPin, ref ptrigsrc);
            return ret;
        }

        public static int FDwfDeviceTriggerPC(int hdwf)
        {
            int ret = PINVOKE.FDwfDeviceTriggerPC(hdwf);
            return ret;
        }

        public static int FDwfDeviceTriggerSlopeInfo(int hdwf, ref int pfsslope)
        {
            int ret = PINVOKE.FDwfDeviceTriggerSlopeInfo(hdwf, ref pfsslope);
            return ret;
        }

        public static int FDwfAnalogInReset(int hdwf)
        {
            int ret = PINVOKE.FDwfAnalogInReset(hdwf);
            return ret;
        }

        public static int FDwfAnalogInConfigure(int hdwf, int fReconfigure, int fStart)
        {
            int ret = PINVOKE.FDwfAnalogInConfigure(hdwf, fReconfigure, fStart);
            return ret;
        }

        public static int FDwfAnalogInStatus(int hdwf, int fReadData, ref byte psts)
        {
            int ret = PINVOKE.FDwfAnalogInStatus(hdwf, fReadData, ref psts);
            return ret;
        }

        public static int FDwfAnalogInStatusSamplesLeft(int hdwf, ref int pcSamplesLeft)
        {
            int ret = PINVOKE.FDwfAnalogInStatusSamplesLeft(hdwf, ref pcSamplesLeft);
            return ret;
        }

        public static int FDwfAnalogInStatusSamplesValid(int hdwf, ref int pcSamplesValid)
        {
            int ret = PINVOKE.FDwfAnalogInStatusSamplesValid(hdwf, ref pcSamplesValid);
            return ret;
        }

        public static int FDwfAnalogInStatusIndexWrite(int hdwf, ref int pidxWrite)
        {
            int ret = PINVOKE.FDwfAnalogInStatusIndexWrite(hdwf, ref pidxWrite);
            return ret;
        }

        public static int FDwfAnalogInStatusAutoTriggered(int hdwf, ref int pfAuto)
        {
            int ret = PINVOKE.FDwfAnalogInStatusAutoTriggered(hdwf, ref pfAuto);
            return ret;
        }

        public static int FDwfAnalogInStatusData(int hdwf, int idxChannel, double[] rgdVoltData, int cdData)
        {
            int ret = PINVOKE.FDwfAnalogInStatusData(hdwf, idxChannel, rgdVoltData, cdData);
            return ret;
        }

        public static int FDwfAnalogInStatusData2(int hdwf, int idxChannel, double[] rgdVoltData, int idxData, int cdData)
        {
            int ret = PINVOKE.FDwfAnalogInStatusData2(hdwf, idxChannel, rgdVoltData, idxData, cdData);
            return ret;
        }

        public static int FDwfAnalogInStatusData16(int hdwf, int idxChannel, short[] rgu16Data, int idxData, int cdData)
        {
            int ret = PINVOKE.FDwfAnalogInStatusData16(hdwf, idxChannel, rgu16Data, idxData, cdData);
            return ret;
        }

        public static int FDwfAnalogInStatusNoise(int hdwf, int idxChannel, double[] rgdMin, ref double rgdMax, int cdData)
        {
            int ret = PINVOKE.FDwfAnalogInStatusNoise(hdwf, idxChannel, rgdMin, ref rgdMax, cdData);
            return ret;
        }

        public static int FDwfAnalogInStatusNoise2(int hdwf, int idxChannel, double[] rgdMin, ref double rgdMax, int idxData, int cdData)
        {
            int ret = PINVOKE.FDwfAnalogInStatusNoise2(hdwf, idxChannel, rgdMin, ref rgdMax, idxData, cdData);
            return ret;
        }

        public static int FDwfAnalogInStatusSample(int hdwf, int idxChannel, ref double pdVoltSample)
        {
            int ret = PINVOKE.FDwfAnalogInStatusSample(hdwf, idxChannel, ref pdVoltSample);
            return ret;
        }

        public static int FDwfAnalogInStatusTime(int hdwf, ref uint psecUtc, ref uint ptick, ref uint pticksPerSecond)
        {
            int ret = PINVOKE.FDwfAnalogInStatusTime(hdwf, ref psecUtc, ref ptick, ref pticksPerSecond);
            return ret;
        }

        public static int FDwfAnalogInStatusRecord(int hdwf, ref int pcdDataAvailable, ref int pcdDataLost, ref int pcdDataCorrupt)
        {
            int ret = PINVOKE.FDwfAnalogInStatusRecord(hdwf, ref pcdDataAvailable, ref pcdDataLost, ref pcdDataCorrupt);
            return ret;
        }

        public static int FDwfAnalogInRecordLengthSet(int hdwf, double sLegth)
        {
            int ret = PINVOKE.FDwfAnalogInRecordLengthSet(hdwf, sLegth);
            return ret;
        }

        public static int FDwfAnalogInRecordLengthGet(int hdwf, ref double psLegth)
        {
            int ret = PINVOKE.FDwfAnalogInRecordLengthGet(hdwf, ref psLegth);
            return ret;
        }

        public static int FDwfAnalogInFrequencyInfo(int hdwf, ref double phzMin, ref double phzMax)
        {
            int ret = PINVOKE.FDwfAnalogInFrequencyInfo(hdwf, ref phzMin, ref phzMax);
            return ret;
        }

        public static int FDwfAnalogInFrequencySet(int hdwf, double hzFrequency)
        {
            int ret = PINVOKE.FDwfAnalogInFrequencySet(hdwf, hzFrequency);
            return ret;
        }

        public static int FDwfAnalogInFrequencyGet(int hdwf, ref double phzFrequency)
        {
            int ret = PINVOKE.FDwfAnalogInFrequencyGet(hdwf, ref phzFrequency);
            return ret;
        }

        public static int FDwfAnalogInBitsInfo(int hdwf, ref int pnBits)
        {
            int ret = PINVOKE.FDwfAnalogInBitsInfo(hdwf, ref pnBits);
            return ret;
        }

        public static int FDwfAnalogInBufferSizeInfo(int hdwf, ref int pnSizeMin, ref int pnSizeMax)
        {
            int ret = PINVOKE.FDwfAnalogInBufferSizeInfo(hdwf, ref pnSizeMin, ref pnSizeMax);
            return ret;
        }

        public static int FDwfAnalogInBufferSizeSet(int hdwf, int nSize)
        {
            int ret = PINVOKE.FDwfAnalogInBufferSizeSet(hdwf, nSize);
            return ret;
        }

        public static int FDwfAnalogInBufferSizeGet(int hdwf, ref int pnSize)
        {
            int ret = PINVOKE.FDwfAnalogInBufferSizeGet(hdwf, ref pnSize);
            return ret;
        }

        public static int FDwfAnalogInNoiseSizeInfo(int hdwf, ref int pnSizeMax)
        {
            int ret = PINVOKE.FDwfAnalogInNoiseSizeInfo(hdwf, ref pnSizeMax);
            return ret;
        }

        public static int FDwfAnalogInNoiseSizeSet(int hdwf, int nSize)
        {
            int ret = PINVOKE.FDwfAnalogInNoiseSizeSet(hdwf, nSize);
            return ret;
        }

        public static int FDwfAnalogInNoiseSizeGet(int hdwf, ref int pnSize)
        {
            int ret = PINVOKE.FDwfAnalogInNoiseSizeGet(hdwf, ref pnSize);
            return ret;
        }

        public static int FDwfAnalogInAcquisitionModeInfo(int hdwf, ref int pfsacqmode)
        {
            int ret = PINVOKE.FDwfAnalogInAcquisitionModeInfo(hdwf, ref pfsacqmode);
            return ret;
        }

        public static int FDwfAnalogInAcquisitionModeSet(int hdwf, int acqmode)
        {
            int ret = PINVOKE.FDwfAnalogInAcquisitionModeSet(hdwf, acqmode);
            return ret;
        }

        public static int FDwfAnalogInAcquisitionModeGet(int hdwf, ref int pacqmode)
        {
            int ret = PINVOKE.FDwfAnalogInAcquisitionModeGet(hdwf, ref pacqmode);
            return ret;
        }

        public static int FDwfAnalogInChannelCount(int hdwf, ref int pcChannel)
        {
            int ret = PINVOKE.FDwfAnalogInChannelCount(hdwf, ref pcChannel);
            return ret;
        }

        public static int FDwfAnalogInChannelEnableSet(int hdwf, int idxChannel, int fEnable)
        {
            int ret = PINVOKE.FDwfAnalogInChannelEnableSet(hdwf, idxChannel, fEnable);
            return ret;
        }

        public static int FDwfAnalogInChannelEnableGet(int hdwf, int idxChannel, ref int pfEnable)
        {
            int ret = PINVOKE.FDwfAnalogInChannelEnableGet(hdwf, idxChannel, ref pfEnable);
            return ret;
        }

        public static int FDwfAnalogInChannelFilterInfo(int hdwf, ref int pfsfilter)
        {
            int ret = PINVOKE.FDwfAnalogInChannelFilterInfo(hdwf, ref pfsfilter);
            return ret;
        }

        public static int FDwfAnalogInChannelFilterSet(int hdwf, int idxChannel, int filter)
        {
            int ret = PINVOKE.FDwfAnalogInChannelFilterSet(hdwf, idxChannel, filter);
            return ret;
        }

        public static int FDwfAnalogInChannelFilterGet(int hdwf, int idxChannel, ref int pfilter)
        {
            int ret = PINVOKE.FDwfAnalogInChannelFilterGet(hdwf, idxChannel, ref pfilter);
            return ret;
        }

        public static int FDwfAnalogInChannelRangeInfo(int hdwf, ref double pvoltsMin, ref double pvoltsMax, ref double pnSteps)
        {
            int ret = PINVOKE.FDwfAnalogInChannelRangeInfo(hdwf, ref pvoltsMin, ref pvoltsMax, ref pnSteps);
            return ret;
        }

        public static int FDwfAnalogInChannelRangeSet(int hdwf, int idxChannel, double voltsRange)
        {
            int ret = PINVOKE.FDwfAnalogInChannelRangeSet(hdwf, idxChannel, voltsRange);
            return ret;
        }

        public static int FDwfAnalogInChannelRangeGet(int hdwf, int idxChannel, ref double pvoltsRange)
        {
            int ret = PINVOKE.FDwfAnalogInChannelRangeGet(hdwf, idxChannel, ref pvoltsRange);
            return ret;
        }

        public static int FDwfAnalogInChannelOffsetInfo(int hdwf, ref double pvoltsMin, ref double pvoltsMax, ref double pnSteps)
        {
            int ret = PINVOKE.FDwfAnalogInChannelOffsetInfo(hdwf, ref pvoltsMin, ref pvoltsMax, ref pnSteps);
            return ret;
        }

        public static int FDwfAnalogInChannelOffsetSet(int hdwf, int idxChannel, double voltOffset)
        {
            int ret = PINVOKE.FDwfAnalogInChannelOffsetSet(hdwf, idxChannel, voltOffset);
            return ret;
        }

        public static int FDwfAnalogInChannelOffsetGet(int hdwf, int idxChannel, ref double pvoltOffset)
        {
            int ret = PINVOKE.FDwfAnalogInChannelOffsetGet(hdwf, idxChannel, ref pvoltOffset);
            return ret;
        }

        public static int FDwfAnalogInChannelAttenuationSet(int hdwf, int idxChannel, double xAttenuation)
        {
            int ret = PINVOKE.FDwfAnalogInChannelAttenuationSet(hdwf, idxChannel, xAttenuation);
            return ret;
        }

        public static int FDwfAnalogInChannelAttenuationGet(int hdwf, int idxChannel, ref double pxAttenuation)
        {
            int ret = PINVOKE.FDwfAnalogInChannelAttenuationGet(hdwf, idxChannel, ref pxAttenuation);
            return ret;
        }

        public static int FDwfAnalogInChannelCouplingInfo(int hdwf, ref int pfscoupling)
        {
            int ret = PINVOKE.FDwfAnalogInChannelCouplingInfo(hdwf, ref pfscoupling);
            return ret;
        }

        public static int FDwfAnalogInChannelCouplingSet(int hdwf, int idxChannel, int coupling)
        {
            int ret = PINVOKE.FDwfAnalogInChannelCouplingSet(hdwf, idxChannel, coupling);
            return ret;
        }

        public static int FDwfAnalogInChannelCouplingGet(int hdwf, int idxChannel, ref int pcoupling)
        {
            int ret = PINVOKE.FDwfAnalogInChannelCouplingGet(hdwf, idxChannel, ref pcoupling);
            return ret;
        }

        public static int FDwfAnalogImpedanceInfo(int hdwf, ref int fSupported)
        {
            int ret = PINVOKE.FDwfAnalogImpedanceInfo(hdwf, ref fSupported);
            return ret;
        }

        public static int FDwfAnalogImpedanceEnableSet(int hdwf, int fEnable)
        {
            int ret = PINVOKE.FDwfAnalogImpedanceEnableSet(hdwf, fEnable);
            return ret;
        }

        public static int FDwfAnalogImpedanceEnableGet(int hdwf, ref int pfEnable)
        {
            int ret = PINVOKE.FDwfAnalogImpedanceEnableGet(hdwf, ref pfEnable);
            return ret;
        }

        public static int FDwfAnalogImpedanceReferenceInfo(int hdwf, ref double pOhmMin, ref double pOhmMax, ref double pnSteps)
        {
            int ret = PINVOKE.FDwfAnalogImpedanceReferenceInfo(hdwf, ref pOhmMin, ref pOhmMax, ref pnSteps);
            return ret;
        }

        public static int FDwfAnalogImpedanceReferenceSet(int hdwf, double ohms)
        {
            int ret = PINVOKE.FDwfAnalogImpedanceReferenceSet(hdwf, ohms);
            return ret;
        }

        public static int FDwfAnalogImpedanceReferenceGet(int hdwf, ref double pohms)
        {
            int ret = PINVOKE.FDwfAnalogImpedanceReferenceGet(hdwf, ref pohms);
            return ret;
        }

        public static int FDwfAnalogInTriggerSourceSet(int hdwf, byte trigsrc)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerSourceSet(hdwf, trigsrc);
            return ret;
        }

        public static int FDwfAnalogInTriggerSourceGet(int hdwf, ref byte ptrigsrc)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerSourceGet(hdwf, ref ptrigsrc);
            return ret;
        }

        public static int FDwfAnalogInTriggerPositionInfo(int hdwf, ref double psecMin, ref double psecMax, ref double pnSteps)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerPositionInfo(hdwf, ref psecMin, ref psecMax, ref pnSteps);
            return ret;
        }

        public static int FDwfAnalogInTriggerPositionSet(int hdwf, double secPosition)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerPositionSet(hdwf, secPosition);
            return ret;
        }

        public static int FDwfAnalogInTriggerPositionGet(int hdwf, ref double psecPosition)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerPositionGet(hdwf, ref psecPosition);
            return ret;
        }

        public static int FDwfAnalogInTriggerPositionStatus(int hdwf, ref double psecPosition)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerPositionStatus(hdwf, ref psecPosition);
            return ret;
        }

        public static int FDwfAnalogInTriggerAutoTimeoutInfo(int hdwf, ref double psecMin, ref double psecMax, ref double pnSteps)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerAutoTimeoutInfo(hdwf, ref psecMin, ref psecMax, ref pnSteps);
            return ret;
        }

        public static int FDwfAnalogInTriggerAutoTimeoutSet(int hdwf, double secTimeout)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerAutoTimeoutSet(hdwf, secTimeout);
            return ret;
        }

        public static int FDwfAnalogInTriggerAutoTimeoutGet(int hdwf, ref double psecTimeout)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerAutoTimeoutGet(hdwf, ref psecTimeout);
            return ret;
        }

        public static int FDwfAnalogInTriggerHoldOffInfo(int hdwf, ref double psecMin, ref double psecMax, ref double pnStep)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerHoldOffInfo(hdwf, ref psecMin, ref psecMax, ref pnStep);
            return ret;
        }

        public static int FDwfAnalogInTriggerHoldOffSet(int hdwf, double secHoldOff)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerHoldOffSet(hdwf, secHoldOff);
            return ret;
        }

        public static int FDwfAnalogInTriggerHoldOffGet(int hdwf, ref double psecHoldOff)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerHoldOffGet(hdwf, ref psecHoldOff);
            return ret;
        }

        public static int FDwfAnalogInTriggerTypeInfo(int hdwf, ref int pfstrigtype)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerTypeInfo(hdwf, ref pfstrigtype);
            return ret;
        }

        public static int FDwfAnalogInTriggerTypeSet(int hdwf, int trigtype)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerTypeSet(hdwf, trigtype);
            return ret;
        }

        public static int FDwfAnalogInTriggerTypeGet(int hdwf, ref int ptrigtype)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerTypeGet(hdwf, ref ptrigtype);
            return ret;
        }

        public static int FDwfAnalogInTriggerChannelInfo(int hdwf, ref int pidxMin, ref int pidxMax)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerChannelInfo(hdwf, ref pidxMin, ref pidxMax);
            return ret;
        }

        public static int FDwfAnalogInTriggerChannelSet(int hdwf, int idxChannel)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerChannelSet(hdwf, idxChannel);
            return ret;
        }

        public static int FDwfAnalogInTriggerChannelGet(int hdwf, ref int pidxChannel)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerChannelGet(hdwf, ref pidxChannel);
            return ret;
        }

        public static int FDwfAnalogInTriggerFilterInfo(int hdwf, ref int pfsfilter)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerFilterInfo(hdwf, ref pfsfilter);
            return ret;
        }

        public static int FDwfAnalogInTriggerFilterSet(int hdwf, int filter)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerFilterSet(hdwf, filter);
            return ret;
        }

        public static int FDwfAnalogInTriggerFilterGet(int hdwf, ref int pfilter)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerFilterGet(hdwf, ref pfilter);
            return ret;
        }

        public static int FDwfAnalogInTriggerLevelInfo(int hdwf, ref double pvoltsMin, ref double pvoltsMax, ref double pnSteps)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerLevelInfo(hdwf, ref pvoltsMin, ref pvoltsMax, ref pnSteps);
            return ret;
        }

        public static int FDwfAnalogInTriggerLevelSet(int hdwf, double voltsLevel)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerLevelSet(hdwf, voltsLevel);
            return ret;
        }

        public static int FDwfAnalogInTriggerLevelGet(int hdwf, ref double pvoltsLevel)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerLevelGet(hdwf, ref pvoltsLevel);
            return ret;
        }

        public static int FDwfAnalogInTriggerHysteresisInfo(int hdwf, ref double pvoltsMin, ref double pvoltsMax, ref double pnSteps)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerHysteresisInfo(hdwf, ref pvoltsMin, ref pvoltsMax, ref pnSteps);
            return ret;
        }

        public static int FDwfAnalogInTriggerHysteresisSet(int hdwf, double voltsLevel)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerHysteresisSet(hdwf, voltsLevel);
            return ret;
        }

        public static int FDwfAnalogInTriggerHysteresisGet(int hdwf, ref double pvoltsHysteresis)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerHysteresisGet(hdwf, ref pvoltsHysteresis);
            return ret;
        }

        public static int FDwfAnalogInTriggerConditionInfo(int hdwf, ref int pfstrigcond)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerConditionInfo(hdwf, ref pfstrigcond);
            return ret;
        }

        public static int FDwfAnalogInTriggerConditionSet(int hdwf, int trigcond)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerConditionSet(hdwf, trigcond);
            return ret;
        }

        public static int FDwfAnalogInTriggerConditionGet(int hdwf, ref int ptrigcond)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerConditionGet(hdwf, ref ptrigcond);
            return ret;
        }

        public static int FDwfAnalogInTriggerLengthInfo(int hdwf, ref double psecMin, ref double psecMax, ref double pnSteps)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerLengthInfo(hdwf, ref psecMin, ref psecMax, ref pnSteps);
            return ret;
        }

        public static int FDwfAnalogInTriggerLengthSet(int hdwf, double secLength)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerLengthSet(hdwf, secLength);
            return ret;
        }

        public static int FDwfAnalogInTriggerLengthGet(int hdwf, ref double psecLength)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerLengthGet(hdwf, ref psecLength);
            return ret;
        }

        public static int FDwfAnalogInTriggerLengthConditionInfo(int hdwf, ref int pfstriglen)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerLengthConditionInfo(hdwf, ref pfstriglen);
            return ret;
        }

        public static int FDwfAnalogInTriggerLengthConditionSet(int hdwf, int triglen)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerLengthConditionSet(hdwf, triglen);
            return ret;
        }

        public static int FDwfAnalogInTriggerLengthConditionGet(int hdwf, ref int ptriglen)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerLengthConditionGet(hdwf, ref ptriglen);
            return ret;
        }

        public static int FDwfAnalogInTriggerForce(int hdwf)
        {
            int ret = PINVOKE.FDwfAnalogInTriggerForce(hdwf);
            return ret;
        }

        public static int FDwfAnalogOutCount(int hdwf, ref int pcChannel)
        {
            int ret = PINVOKE.FDwfAnalogOutCount(hdwf, ref pcChannel);
            return ret;
        }

        public static int FDwfAnalogOutMasterSet(int hdwf, int idxChannel, int idxMaster)
        {
            int ret = PINVOKE.FDwfAnalogOutMasterSet(hdwf, idxChannel, idxMaster);
            return ret;
        }

        public static int FDwfAnalogOutMasterGet(int hdwf, int idxChannel, ref int pidxMaster)
        {
            int ret = PINVOKE.FDwfAnalogOutMasterGet(hdwf, idxChannel, ref pidxMaster);
            return ret;
        }

        public static int FDwfAnalogOutTriggerSourceSet(int hdwf, int idxChannel, byte trigsrc)
        {
            int ret = PINVOKE.FDwfAnalogOutTriggerSourceSet(hdwf, idxChannel, trigsrc);
            return ret;
        }

        public static int FDwfAnalogOutTriggerSourceGet(int hdwf, int idxChannel, ref byte ptrigsrc)
        {
            int ret = PINVOKE.FDwfAnalogOutTriggerSourceGet(hdwf, idxChannel, ref ptrigsrc);
            return ret;
        }

        public static int FDwfAnalogOutTriggerSlopeSet(int hdwf, int idxChannel, int slope)
        {
            int ret = PINVOKE.FDwfAnalogOutTriggerSlopeSet(hdwf, idxChannel, slope);
            return ret;
        }

        public static int FDwfAnalogOutTriggerSlopeGet(int hdwf, int idxChannel, ref int pslope)
        {
            int ret = PINVOKE.FDwfAnalogOutTriggerSlopeGet(hdwf, idxChannel, ref pslope);
            return ret;
        }

        public static int FDwfAnalogOutRunInfo(int hdwf, int idxChannel, ref double psecMin, ref double psecMax)
        {
            int ret = PINVOKE.FDwfAnalogOutRunInfo(hdwf, idxChannel, ref psecMin, ref psecMax);
            return ret;
        }

        public static int FDwfAnalogOutRunSet(int hdwf, int idxChannel, double secRun)
        {
            int ret = PINVOKE.FDwfAnalogOutRunSet(hdwf, idxChannel, secRun);
            return ret;
        }

        public static int FDwfAnalogOutRunGet(int hdwf, int idxChannel, ref double psecRun)
        {
            int ret = PINVOKE.FDwfAnalogOutRunGet(hdwf, idxChannel, ref psecRun);
            return ret;
        }

        public static int FDwfAnalogOutRunStatus(int hdwf, int idxChannel, ref double psecRun)
        {
            int ret = PINVOKE.FDwfAnalogOutRunStatus(hdwf, idxChannel, ref psecRun);
            return ret;
        }

        public static int FDwfAnalogOutWaitInfo(int hdwf, int idxChannel, ref double psecMin, ref double psecMax)
        {
            int ret = PINVOKE.FDwfAnalogOutWaitInfo(hdwf, idxChannel, ref psecMin, ref psecMax);
            return ret;
        }

        public static int FDwfAnalogOutWaitSet(int hdwf, int idxChannel, double secWait)
        {
            int ret = PINVOKE.FDwfAnalogOutWaitSet(hdwf, idxChannel, secWait);
            return ret;
        }

        public static int FDwfAnalogOutWaitGet(int hdwf, int idxChannel, ref double psecWait)
        {
            int ret = PINVOKE.FDwfAnalogOutWaitGet(hdwf, idxChannel, ref psecWait);
            return ret;
        }

        public static int FDwfAnalogOutRepeatInfo(int hdwf, int idxChannel, ref int pnMin, ref int pnMax)
        {
            int ret = PINVOKE.FDwfAnalogOutRepeatInfo(hdwf, idxChannel, ref pnMin, ref pnMax);
            return ret;
        }

        public static int FDwfAnalogOutRepeatSet(int hdwf, int idxChannel, int cRepeat)
        {
            int ret = PINVOKE.FDwfAnalogOutRepeatSet(hdwf, idxChannel, cRepeat);
            return ret;
        }

        public static int FDwfAnalogOutRepeatGet(int hdwf, int idxChannel, ref int pcRepeat)
        {
            int ret = PINVOKE.FDwfAnalogOutRepeatGet(hdwf, idxChannel, ref pcRepeat);
            return ret;
        }

        public static int FDwfAnalogOutRepeatStatus(int hdwf, int idxChannel, ref int pcRepeat)
        {
            int ret = PINVOKE.FDwfAnalogOutRepeatStatus(hdwf, idxChannel, ref pcRepeat);
            return ret;
        }

        public static int FDwfAnalogOutRepeatTriggerSet(int hdwf, int idxChannel, int fRepeatTrigger)
        {
            int ret = PINVOKE.FDwfAnalogOutRepeatTriggerSet(hdwf, idxChannel, fRepeatTrigger);
            return ret;
        }

        public static int FDwfAnalogOutRepeatTriggerGet(int hdwf, int idxChannel, ref int pfRepeatTrigger)
        {
            int ret = PINVOKE.FDwfAnalogOutRepeatTriggerGet(hdwf, idxChannel, ref pfRepeatTrigger);
            return ret;
        }

        public static int FDwfAnalogOutLimitationInfo(int hdwf, int idxChannel, ref double pMin, ref double pMax)
        {
            int ret = PINVOKE.FDwfAnalogOutLimitationInfo(hdwf, idxChannel, ref pMin, ref pMax);
            return ret;
        }

        public static int FDwfAnalogOutLimitationSet(int hdwf, int idxChannel, double limit)
        {
            int ret = PINVOKE.FDwfAnalogOutLimitationSet(hdwf, idxChannel, limit);
            return ret;
        }

        public static int FDwfAnalogOutLimitationGet(int hdwf, int idxChannel, ref double plimit)
        {
            int ret = PINVOKE.FDwfAnalogOutLimitationGet(hdwf, idxChannel, ref plimit);
            return ret;
        }

        public static int FDwfAnalogOutModeSet(int hdwf, int idxChannel, int mode)
        {
            int ret = PINVOKE.FDwfAnalogOutModeSet(hdwf, idxChannel, mode);
            return ret;
        }

        public static int FDwfAnalogOutModeGet(int hdwf, int idxChannel, ref int pmode)
        {
            int ret = PINVOKE.FDwfAnalogOutModeGet(hdwf, idxChannel, ref pmode);
            return ret;
        }

        public static int FDwfAnalogOutIdleInfo(int hdwf, int idxChannel, ref int pfsidle)
        {
            int ret = PINVOKE.FDwfAnalogOutIdleInfo(hdwf, idxChannel, ref pfsidle);
            return ret;
        }

        public static int FDwfAnalogOutIdleSet(int hdwf, int idxChannel, int idle)
        {
            int ret = PINVOKE.FDwfAnalogOutIdleSet(hdwf, idxChannel, idle);
            return ret;
        }

        public static int FDwfAnalogOutIdleGet(int hdwf, int idxChannel, ref int pidle)
        {
            int ret = PINVOKE.FDwfAnalogOutIdleGet(hdwf, idxChannel, ref pidle);
            return ret;
        }

        public static int FDwfAnalogOutNodeInfo(int hdwf, int idxChannel, ref int pfsnode)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeInfo(hdwf, idxChannel, ref pfsnode);
            return ret;
        }

        public static int FDwfAnalogOutNodeEnableSet(int hdwf, int idxChannel, int node, int fEnable)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeEnableSet(hdwf, idxChannel, node, fEnable);
            return ret;
        }

        public static int FDwfAnalogOutNodeEnableGet(int hdwf, int idxChannel, int node, ref int pfEnable)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeEnableGet(hdwf, idxChannel, node, ref pfEnable);
            return ret;
        }

        public static int FDwfAnalogOutNodeFunctionInfo(int hdwf, int idxChannel, int node, ref int pfsfunc)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeFunctionInfo(hdwf, idxChannel, node, ref pfsfunc);
            return ret;
        }

        public static int FDwfAnalogOutNodeFunctionSet(int hdwf, int idxChannel, int node, byte func)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeFunctionSet(hdwf, idxChannel, node, func);
            return ret;
        }

        public static int FDwfAnalogOutNodeFunctionGet(int hdwf, int idxChannel, int node, ref byte pfunc)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeFunctionGet(hdwf, idxChannel, node, ref pfunc);
            return ret;
        }

        public static int FDwfAnalogOutNodeFrequencyInfo(int hdwf, int idxChannel, int node, ref double phzMin, ref double phzMax)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeFrequencyInfo(hdwf, idxChannel, node, ref phzMin, ref phzMax);
            return ret;
        }

        public static int FDwfAnalogOutNodeFrequencySet(int hdwf, int idxChannel, int node, double hzFrequency)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeFrequencySet(hdwf, idxChannel, node, hzFrequency);
            return ret;
        }

        public static int FDwfAnalogOutNodeFrequencyGet(int hdwf, int idxChannel, int node, ref double phzFrequency)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeFrequencyGet(hdwf, idxChannel, node, ref phzFrequency);
            return ret;
        }

        public static int FDwfAnalogOutNodeAmplitudeInfo(int hdwf, int idxChannel, int node, ref double pMin, ref double pMax)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeAmplitudeInfo(hdwf, idxChannel, node, ref pMin, ref pMax);
            return ret;
        }

        public static int FDwfAnalogOutNodeAmplitudeSet(int hdwf, int idxChannel, int node, double vAmplitude)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeAmplitudeSet(hdwf, idxChannel, node, vAmplitude);
            return ret;
        }

        public static int FDwfAnalogOutNodeAmplitudeGet(int hdwf, int idxChannel, int node, ref double pvAmplitude)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeAmplitudeGet(hdwf, idxChannel, node, ref pvAmplitude);
            return ret;
        }

        public static int FDwfAnalogOutNodeOffsetInfo(int hdwf, int idxChannel, int node, ref double pMin, ref double pMax)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeOffsetInfo(hdwf, idxChannel, node, ref pMin, ref pMax);
            return ret;
        }

        public static int FDwfAnalogOutNodeOffsetSet(int hdwf, int idxChannel, int node, double vOffset)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeOffsetSet(hdwf, idxChannel, node, vOffset);
            return ret;
        }

        public static int FDwfAnalogOutNodeOffsetGet(int hdwf, int idxChannel, int node, ref double pvOffset)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeOffsetGet(hdwf, idxChannel, node, ref pvOffset);
            return ret;
        }

        public static int FDwfAnalogOutNodeSymmetryInfo(int hdwf, int idxChannel, int node, ref double ppercentageMin, ref double ppercentageMax)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeSymmetryInfo(hdwf, idxChannel, node, ref ppercentageMin, ref ppercentageMax);
            return ret;
        }

        public static int FDwfAnalogOutNodeSymmetrySet(int hdwf, int idxChannel, int node, double percentageSymmetry)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeSymmetrySet(hdwf, idxChannel, node, percentageSymmetry);
            return ret;
        }

        public static int FDwfAnalogOutNodeSymmetryGet(int hdwf, int idxChannel, int node, ref double ppercentageSymmetry)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeSymmetryGet(hdwf, idxChannel, node, ref ppercentageSymmetry);
            return ret;
        }

        public static int FDwfAnalogOutNodePhaseInfo(int hdwf, int idxChannel, int node, ref double pdegreeMin, ref double pdegreeMax)
        {
            int ret = PINVOKE.FDwfAnalogOutNodePhaseInfo(hdwf, idxChannel, node, ref pdegreeMin, ref pdegreeMax);
            return ret;
        }

        public static int FDwfAnalogOutNodePhaseSet(int hdwf, int idxChannel, int node, double degreePhase)
        {
            int ret = PINVOKE.FDwfAnalogOutNodePhaseSet(hdwf, idxChannel, node, degreePhase);
            return ret;
        }

        public static int FDwfAnalogOutNodePhaseGet(int hdwf, int idxChannel, int node, ref double pdegreePhase)
        {
            int ret = PINVOKE.FDwfAnalogOutNodePhaseGet(hdwf, idxChannel, node, ref pdegreePhase);
            return ret;
        }

        public static int FDwfAnalogOutNodeDataInfo(int hdwf, int idxChannel, int node, ref int pnSamplesMin, ref int pnSamplesMax)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeDataInfo(hdwf, idxChannel, node, ref pnSamplesMin, ref pnSamplesMax);
            return ret;
        }

        public static int FDwfAnalogOutNodeDataSet(int hdwf, int idxChannel, int node, double[] rgdData, int cdData)
        {
            int ret = PINVOKE.FDwfAnalogOutNodeDataSet(hdwf, idxChannel, node, rgdData, cdData);
            return ret;
        }

        public static int FDwfAnalogOutCustomAMFMEnableSet(int hdwf, int idxChannel, int fEnable)
        {
            int ret = PINVOKE.FDwfAnalogOutCustomAMFMEnableSet(hdwf, idxChannel, fEnable);
            return ret;
        }

        public static int FDwfAnalogOutCustomAMFMEnableGet(int hdwf, int idxChannel, ref int pfEnable)
        {
            int ret = PINVOKE.FDwfAnalogOutCustomAMFMEnableGet(hdwf, idxChannel, ref pfEnable);
            return ret;
        }

        public static int FDwfAnalogOutReset(int hdwf, int idxChannel)
        {
            int ret = PINVOKE.FDwfAnalogOutReset(hdwf, idxChannel);
            return ret;
        }

        public static int FDwfAnalogOutConfigure(int hdwf, int idxChannel, int fStart)
        {
            int ret = PINVOKE.FDwfAnalogOutConfigure(hdwf, idxChannel, fStart);
            return ret;
        }

        public static int FDwfAnalogOutStatus(int hdwf, int idxChannel, ref byte psts)
        {
            int ret = PINVOKE.FDwfAnalogOutStatus(hdwf, idxChannel, ref psts);
            return ret;
        }

        public static int FDwfAnalogOutNodePlayStatus(int hdwf, int idxChannel, int node, ref int cdDataFree, ref int cdDataLost, ref int cdDataCorrupted)
        {
            int ret = PINVOKE.FDwfAnalogOutNodePlayStatus(hdwf, idxChannel, node, ref cdDataFree, ref cdDataLost, ref cdDataCorrupted);
            return ret;
        }

        public static int FDwfAnalogOutNodePlayData(int hdwf, int idxChannel, int node, double[] rgdData, int cdData)
        {
            int ret = PINVOKE.FDwfAnalogOutNodePlayData(hdwf, idxChannel, node, rgdData, cdData);
            return ret;
        }

        public static int FDwfAnalogIOReset(int hdwf)
        {
            int ret = PINVOKE.FDwfAnalogIOReset(hdwf);
            return ret;
        }

        public static int FDwfAnalogIOConfigure(int hdwf)
        {
            int ret = PINVOKE.FDwfAnalogIOConfigure(hdwf);
            return ret;
        }

        public static int FDwfAnalogIOStatus(int hdwf)
        {
            int ret = PINVOKE.FDwfAnalogIOStatus(hdwf);
            return ret;
        }

        public static int FDwfAnalogIOEnableInfo(int hdwf, ref int pfSet, ref int pfStatus)
        {
            int ret = PINVOKE.FDwfAnalogIOEnableInfo(hdwf, ref pfSet, ref pfStatus);
            return ret;
        }

        public static int FDwfAnalogIOEnableSet(int hdwf, int fMasterEnable)
        {
            int ret = PINVOKE.FDwfAnalogIOEnableSet(hdwf, fMasterEnable);
            return ret;
        }

        public static int FDwfAnalogIOEnableGet(int hdwf, ref int pfMasterEnable)
        {
            int ret = PINVOKE.FDwfAnalogIOEnableGet(hdwf, ref pfMasterEnable);
            return ret;
        }

        public static int FDwfAnalogIOEnableStatus(int hdwf, ref int pfMasterEnable)
        {
            int ret = PINVOKE.FDwfAnalogIOEnableStatus(hdwf, ref pfMasterEnable);
            return ret;
        }

        public static int FDwfAnalogIOChannelCount(int hdwf, ref int pnChannel)
        {
            int ret = PINVOKE.FDwfAnalogIOChannelCount(hdwf, ref pnChannel);
            return ret;
        }

        public static int FDwfAnalogIOChannelName(int hdwf, int idxChannel, string szName, string szLabel)
        {
            int ret = PINVOKE.FDwfAnalogIOChannelName(hdwf, idxChannel, szName, szLabel);
            return ret;
        }

        public static int FDwfAnalogIOChannelInfo(int hdwf, int idxChannel, ref int pnNodes)
        {
            int ret = PINVOKE.FDwfAnalogIOChannelInfo(hdwf, idxChannel, ref pnNodes);
            return ret;
        }

        public static int FDwfAnalogIOChannelNodeName(int hdwf, int idxChannel, int idxNode, string szNodeName, string szNodeUnits)
        {
            int ret = PINVOKE.FDwfAnalogIOChannelNodeName(hdwf, idxChannel, idxNode, szNodeName, szNodeUnits);
            return ret;
        }

        public static int FDwfAnalogIOChannelNodeInfo(int hdwf, int idxChannel, int idxNode, ref byte panalogio)
        {
            int ret = PINVOKE.FDwfAnalogIOChannelNodeInfo(hdwf, idxChannel, idxNode, ref panalogio);
            return ret;
        }

        public static int FDwfAnalogIOChannelNodeSetInfo(int hdwf, int idxChannel, int idxNode, ref double pmin, ref double pmax, ref int pnSteps)
        {
            int ret = PINVOKE.FDwfAnalogIOChannelNodeSetInfo(hdwf, idxChannel, idxNode, ref pmin, ref pmax, ref pnSteps);
            return ret;
        }

        public static int FDwfAnalogIOChannelNodeSet(int hdwf, int idxChannel, int idxNode, double value)
        {
            int ret = PINVOKE.FDwfAnalogIOChannelNodeSet(hdwf, idxChannel, idxNode, value);
            return ret;
        }

        public static int FDwfAnalogIOChannelNodeGet(int hdwf, int idxChannel, int idxNode, ref double pvalue)
        {
            int ret = PINVOKE.FDwfAnalogIOChannelNodeGet(hdwf, idxChannel, idxNode, ref pvalue);
            return ret;
        }

        public static int FDwfAnalogIOChannelNodeStatusInfo(int hdwf, int idxChannel, int idxNode, ref double pmin, ref double pmax, ref int pnSteps)
        {
            int ret = PINVOKE.FDwfAnalogIOChannelNodeStatusInfo(hdwf, idxChannel, idxNode, ref pmin, ref pmax, ref pnSteps);
            return ret;
        }

        public static int FDwfAnalogIOChannelNodeStatus(int hdwf, int idxChannel, int idxNode, ref double pvalue)
        {
            int ret = PINVOKE.FDwfAnalogIOChannelNodeStatus(hdwf, idxChannel, idxNode, ref pvalue);
            return ret;
        }

        public static int FDwfDigitalIOReset(int hdwf)
        {
            int ret = PINVOKE.FDwfDigitalIOReset(hdwf);
            return ret;
        }

        public static int FDwfDigitalIOConfigure(int hdwf)
        {
            int ret = PINVOKE.FDwfDigitalIOConfigure(hdwf);
            return ret;
        }

        public static int FDwfDigitalIOStatus(int hdwf)
        {
            int ret = PINVOKE.FDwfDigitalIOStatus(hdwf);
            return ret;
        }

        public static int FDwfDigitalIOOutputEnableInfo(int hdwf, ref uint pfsOutputEnableMask)
        {
            int ret = PINVOKE.FDwfDigitalIOOutputEnableInfo(hdwf, ref pfsOutputEnableMask);
            return ret;
        }

        public static int FDwfDigitalIOOutputEnableSet(int hdwf, uint fsOutputEnable)
        {
            int ret = PINVOKE.FDwfDigitalIOOutputEnableSet(hdwf, fsOutputEnable);
            return ret;
        }

        public static int FDwfDigitalIOOutputEnableGet(int hdwf, ref uint pfsOutputEnable)
        {
            int ret = PINVOKE.FDwfDigitalIOOutputEnableGet(hdwf, ref pfsOutputEnable);
            return ret;
        }

        public static int FDwfDigitalIOOutputInfo(int hdwf, ref uint pfsOutputMask)
        {
            int ret = PINVOKE.FDwfDigitalIOOutputInfo(hdwf, ref pfsOutputMask);
            return ret;
        }

        public static int FDwfDigitalIOOutputSet(int hdwf, uint fsOutput)
        {
            int ret = PINVOKE.FDwfDigitalIOOutputSet(hdwf, fsOutput);
            return ret;
        }

        public static int FDwfDigitalIOOutputGet(int hdwf, ref uint pfsOutput)
        {
            int ret = PINVOKE.FDwfDigitalIOOutputGet(hdwf, ref pfsOutput);
            return ret;
        }

        public static int FDwfDigitalIOInputInfo(int hdwf, ref uint pfsInputMask)
        {
            int ret = PINVOKE.FDwfDigitalIOInputInfo(hdwf, ref pfsInputMask);
            return ret;
        }

        public static int FDwfDigitalIOInputStatus(int hdwf, ref uint pfsInput)
        {
            int ret = PINVOKE.FDwfDigitalIOInputStatus(hdwf, ref pfsInput);
            return ret;
        }

        public static int FDwfDigitalIOOutputEnableInfo64(int hdwf, ref ulong pfsOutputEnableMask)
        {
            int ret = PINVOKE.FDwfDigitalIOOutputEnableInfo64(hdwf, ref pfsOutputEnableMask);
            return ret;
        }

        public static int FDwfDigitalIOOutputEnableSet64(int hdwf, ulong fsOutputEnable)
        {
            int ret = PINVOKE.FDwfDigitalIOOutputEnableSet64(hdwf, fsOutputEnable);
            return ret;
        }

        public static int FDwfDigitalIOOutputEnableGet64(int hdwf, ref ulong pfsOutputEnable)
        {
            int ret = PINVOKE.FDwfDigitalIOOutputEnableGet64(hdwf, ref pfsOutputEnable);
            return ret;
        }

        public static int FDwfDigitalIOOutputInfo64(int hdwf, ref ulong pfsOutputMask)
        {
            int ret = PINVOKE.FDwfDigitalIOOutputInfo64(hdwf, ref pfsOutputMask);
            return ret;
        }

        public static int FDwfDigitalIOOutputSet64(int hdwf, ulong fsOutput)
        {
            int ret = PINVOKE.FDwfDigitalIOOutputSet64(hdwf, fsOutput);
            return ret;
        }

        public static int FDwfDigitalIOOutputGet64(int hdwf, ref ulong pfsOutput)
        {
            int ret = PINVOKE.FDwfDigitalIOOutputGet64(hdwf, ref pfsOutput);
            return ret;
        }

        public static int FDwfDigitalIOInputInfo64(int hdwf, ref ulong pfsInputMask)
        {
            int ret = PINVOKE.FDwfDigitalIOInputInfo64(hdwf, ref pfsInputMask);
            return ret;
        }

        public static int FDwfDigitalIOInputStatus64(int hdwf, ref ulong pfsInput)
        {
            int ret = PINVOKE.FDwfDigitalIOInputStatus64(hdwf, ref pfsInput);
            return ret;
        }

        public static int FDwfDigitalInReset(int hdwf)
        {
            int ret = PINVOKE.FDwfDigitalInReset(hdwf);
            return ret;
        }

        public static int FDwfDigitalInConfigure(int hdwf, int fReconfigure, int fStart)
        {
            int ret = PINVOKE.FDwfDigitalInConfigure(hdwf, fReconfigure, fStart);
            return ret;
        }

        public static int FDwfDigitalInStatus(int hdwf, int fReadData, ref byte psts)
        {
            int ret = PINVOKE.FDwfDigitalInStatus(hdwf, fReadData, ref psts);
            return ret;
        }

        public static int FDwfDigitalInStatusSamplesLeft(int hdwf, ref int pcSamplesLeft)
        {
            int ret = PINVOKE.FDwfDigitalInStatusSamplesLeft(hdwf, ref pcSamplesLeft);
            return ret;
        }

        public static int FDwfDigitalInStatusSamplesValid(int hdwf, ref int pcSamplesValid)
        {
            int ret = PINVOKE.FDwfDigitalInStatusSamplesValid(hdwf, ref pcSamplesValid);
            return ret;
        }

        public static int FDwfDigitalInStatusIndexWrite(int hdwf, ref int pidxWrite)
        {
            int ret = PINVOKE.FDwfDigitalInStatusIndexWrite(hdwf, ref pidxWrite);
            return ret;
        }

        public static int FDwfDigitalInStatusAutoTriggered(int hdwf, ref int pfAuto)
        {
            int ret = PINVOKE.FDwfDigitalInStatusAutoTriggered(hdwf, ref pfAuto);
            return ret;
        }

        public static int FDwfDigitalInStatusData(int hdwf, byte[] rgData, int countOfDataBytes)
        {
            int ret = PINVOKE.FDwfDigitalInStatusData(hdwf, rgData, countOfDataBytes);
            return ret;
        }

        public static int FDwfDigitalInStatusData2(int hdwf, byte[] rgData, int idxSample, int countOfDataBytes)
        {
            int ret = PINVOKE.FDwfDigitalInStatusData2(hdwf, rgData, idxSample, countOfDataBytes);
            return ret;
        }

        public static int FDwfDigitalInStatusNoise2(int hdwf, byte[] rgData, int idxSample, int countOfDataBytes)
        {
            int ret = PINVOKE.FDwfDigitalInStatusNoise2(hdwf, rgData, idxSample, countOfDataBytes);
            return ret;
        }

        public static int FDwfDigitalInStatusRecord(int hdwf, ref int pcdDataAvailable, ref int pcdDataLost, ref int pcdDataCorrupt)
        {
            int ret = PINVOKE.FDwfDigitalInStatusRecord(hdwf, ref pcdDataAvailable, ref pcdDataLost, ref pcdDataCorrupt);
            return ret;
        }

        public static int FDwfDigitalInInternalClockInfo(int hdwf, ref double phzFreq)
        {
            int ret = PINVOKE.FDwfDigitalInInternalClockInfo(hdwf, ref phzFreq);
            return ret;
        }

        public static int FDwfDigitalInClockSourceInfo(int hdwf, ref int pfsDwfDigitalInClockSource)
        {
            int ret = PINVOKE.FDwfDigitalInClockSourceInfo(hdwf, ref pfsDwfDigitalInClockSource);
            return ret;
        }

        public static int FDwfDigitalInClockSourceSet(int hdwf, int v)
        {
            int ret = PINVOKE.FDwfDigitalInClockSourceSet(hdwf, v);
            return ret;
        }

        public static int FDwfDigitalInClockSourceGet(int hdwf, ref int pv)
        {
            int ret = PINVOKE.FDwfDigitalInClockSourceGet(hdwf, ref pv);
            return ret;
        }

        public static int FDwfDigitalInDividerInfo(int hdwf, ref uint pdivMax)
        {
            int ret = PINVOKE.FDwfDigitalInDividerInfo(hdwf, ref pdivMax);
            return ret;
        }

        public static int FDwfDigitalInDividerSet(int hdwf, uint div)
        {
            int ret = PINVOKE.FDwfDigitalInDividerSet(hdwf, div);
            return ret;
        }

        public static int FDwfDigitalInDividerGet(int hdwf, ref uint pdiv)
        {
            int ret = PINVOKE.FDwfDigitalInDividerGet(hdwf, ref pdiv);
            return ret;
        }

        public static int FDwfDigitalInBitsInfo(int hdwf, ref int pnBits)
        {
            int ret = PINVOKE.FDwfDigitalInBitsInfo(hdwf, ref pnBits);
            return ret;
        }

        public static int FDwfDigitalInSampleFormatSet(int hdwf, int nBits)
        {
            int ret = PINVOKE.FDwfDigitalInSampleFormatSet(hdwf, nBits);
            return ret;
        }

        public static int FDwfDigitalInSampleFormatGet(int hdwf, ref int pnBits)
        {
            int ret = PINVOKE.FDwfDigitalInSampleFormatGet(hdwf, ref pnBits);
            return ret;
        }

        public static int FDwfDigitalInInputOrderSet(int hdwf, bool fDioFirst)
        {
            int ret = PINVOKE.FDwfDigitalInInputOrderSet(hdwf, fDioFirst);
            return ret;
        }

        public static int FDwfDigitalInBufferSizeInfo(int hdwf, ref int pnSizeMax)
        {
            int ret = PINVOKE.FDwfDigitalInBufferSizeInfo(hdwf, ref pnSizeMax);
            return ret;
        }

        public static int FDwfDigitalInBufferSizeSet(int hdwf, int nSize)
        {
            int ret = PINVOKE.FDwfDigitalInBufferSizeSet(hdwf, nSize);
            return ret;
        }

        public static int FDwfDigitalInBufferSizeGet(int hdwf, ref int pnSize)
        {
            int ret = PINVOKE.FDwfDigitalInBufferSizeGet(hdwf, ref pnSize);
            return ret;
        }

        public static int FDwfDigitalInSampleModeInfo(int hdwf, ref int pfsDwfDigitalInSampleMode)
        {
            int ret = PINVOKE.FDwfDigitalInSampleModeInfo(hdwf, ref pfsDwfDigitalInSampleMode);
            return ret;
        }

        public static int FDwfDigitalInSampleModeSet(int hdwf, int v)
        {
            int ret = PINVOKE.FDwfDigitalInSampleModeSet(hdwf, v);
            return ret;
        }

        public static int FDwfDigitalInSampleModeGet(int hdwf, ref int pv)
        {
            int ret = PINVOKE.FDwfDigitalInSampleModeGet(hdwf, ref pv);
            return ret;
        }

        public static int FDwfDigitalInAcquisitionModeInfo(int hdwf, ref int pfsacqmode)
        {
            int ret = PINVOKE.FDwfDigitalInAcquisitionModeInfo(hdwf, ref pfsacqmode);
            return ret;
        }

        public static int FDwfDigitalInAcquisitionModeSet(int hdwf, int acqmode)
        {
            int ret = PINVOKE.FDwfDigitalInAcquisitionModeSet(hdwf, acqmode);
            return ret;
        }

        public static int FDwfDigitalInAcquisitionModeGet(int hdwf, ref int pacqmode)
        {
            int ret = PINVOKE.FDwfDigitalInAcquisitionModeGet(hdwf, ref pacqmode);
            return ret;
        }

        public static int FDwfDigitalInMixedSet(int hdwf, int fEnable)
        {
            int ret = PINVOKE.FDwfDigitalInMixedSet(hdwf, fEnable);
            return ret;
        }

        public static int FDwfDigitalInTriggerSourceSet(int hdwf, byte trigsrc)
        {
            int ret = PINVOKE.FDwfDigitalInTriggerSourceSet(hdwf, trigsrc);
            return ret;
        }

        public static int FDwfDigitalInTriggerSourceGet(int hdwf, ref byte ptrigsrc)
        {
            int ret = PINVOKE.FDwfDigitalInTriggerSourceGet(hdwf, ref ptrigsrc);
            return ret;
        }

        public static int FDwfDigitalInTriggerSlopeSet(int hdwf, int slope)
        {
            int ret = PINVOKE.FDwfDigitalInTriggerSlopeSet(hdwf, slope);
            return ret;
        }

        public static int FDwfDigitalInTriggerSlopeGet(int hdwf, ref int pslope)
        {
            int ret = PINVOKE.FDwfDigitalInTriggerSlopeGet(hdwf, ref pslope);
            return ret;
        }

        public static int FDwfDigitalInTriggerPositionInfo(int hdwf, ref uint pnSamplesAfterTriggerMax)
        {
            int ret = PINVOKE.FDwfDigitalInTriggerPositionInfo(hdwf, ref pnSamplesAfterTriggerMax);
            return ret;
        }

        public static int FDwfDigitalInTriggerPositionSet(int hdwf, uint cSamplesAfterTrigger)
        {
            int ret = PINVOKE.FDwfDigitalInTriggerPositionSet(hdwf, cSamplesAfterTrigger);
            return ret;
        }

        public static int FDwfDigitalInTriggerPositionGet(int hdwf, ref uint pcSamplesAfterTrigger)
        {
            int ret = PINVOKE.FDwfDigitalInTriggerPositionGet(hdwf, ref pcSamplesAfterTrigger);
            return ret;
        }

        public static int FDwfDigitalInTriggerPrefillSet(int hdwf, uint cSamplesBeforeTrigger)
        {
            int ret = PINVOKE.FDwfDigitalInTriggerPrefillSet(hdwf, cSamplesBeforeTrigger);
            return ret;
        }

        public static int FDwfDigitalInTriggerPrefillGet(int hdwf, ref uint pcSamplesBeforeTrigger)
        {
            int ret = PINVOKE.FDwfDigitalInTriggerPrefillGet(hdwf, ref pcSamplesBeforeTrigger);
            return ret;
        }

        public static int FDwfDigitalInTriggerAutoTimeoutInfo(int hdwf, ref double psecMin, ref double psecMax, ref double pnSteps)
        {
            int ret = PINVOKE.FDwfDigitalInTriggerAutoTimeoutInfo(hdwf, ref psecMin, ref psecMax, ref pnSteps);
            return ret;
        }

        public static int FDwfDigitalInTriggerAutoTimeoutSet(int hdwf, double secTimeout)
        {
            int ret = PINVOKE.FDwfDigitalInTriggerAutoTimeoutSet(hdwf, secTimeout);
            return ret;
        }

        public static int FDwfDigitalInTriggerAutoTimeoutGet(int hdwf, ref double psecTimeout)
        {
            int ret = PINVOKE.FDwfDigitalInTriggerAutoTimeoutGet(hdwf, ref psecTimeout);
            return ret;
        }

        public static int FDwfDigitalInTriggerInfo(int hdwf, ref uint pfsLevelLow, ref uint pfsLevelHigh, ref uint pfsEdgeRise, ref uint pfsEdgeFall)
        {
            int ret = PINVOKE.FDwfDigitalInTriggerInfo(hdwf, ref pfsLevelLow, ref pfsLevelHigh, ref pfsEdgeRise, ref pfsEdgeFall);
            return ret;
        }

        public static int FDwfDigitalInTriggerSet(int hdwf, uint fsLevelLow, uint fsLevelHigh, uint fsEdgeRise, uint fsEdgeFall)
        {
            int ret = PINVOKE.FDwfDigitalInTriggerSet(hdwf, fsLevelLow, fsLevelHigh, fsEdgeRise, fsEdgeFall);
            return ret;
        }

        public static int FDwfDigitalInTriggerGet(int hdwf, ref uint pfsLevelLow, ref uint pfsLevelHigh, ref uint pfsEdgeRise, ref uint pfsEdgeFall)
        {
            int ret = PINVOKE.FDwfDigitalInTriggerGet(hdwf, ref pfsLevelLow, ref pfsLevelHigh, ref pfsEdgeRise, ref pfsEdgeFall);
            return ret;
        }

        public static int FDwfDigitalInTriggerResetSet(int hdwf, uint fsLevelLow, uint fsLevelHigh, uint fsEdgeRise, uint fsEdgeFall)
        {
            int ret = PINVOKE.FDwfDigitalInTriggerResetSet(hdwf, fsLevelLow, fsLevelHigh, fsEdgeRise, fsEdgeFall);
            return ret;
        }

        public static int FDwfDigitalInTriggerCountSet(int hdwf, int cCount, int fRestart)
        {
            int ret = PINVOKE.FDwfDigitalInTriggerCountSet(hdwf, cCount, fRestart);
            return ret;
        }

        public static int FDwfDigitalInTriggerLengthSet(int hdwf, double secMin, double secMax, int idxSync)
        {
            int ret = PINVOKE.FDwfDigitalInTriggerLengthSet(hdwf, secMin, secMax, idxSync);
            return ret;
        }

        public static int FDwfDigitalInTriggerMatchSet(int hdwf, int iPin, uint fsMask, uint fsValue, int cBitStuffing)
        {
            int ret = PINVOKE.FDwfDigitalInTriggerMatchSet(hdwf, iPin, fsMask, fsValue, cBitStuffing);
            return ret;
        }

        public static int FDwfDigitalOutReset(int hdwf)
        {
            int ret = PINVOKE.FDwfDigitalOutReset(hdwf);
            return ret;
        }

        public static int FDwfDigitalOutConfigure(int hdwf, int fStart)
        {
            int ret = PINVOKE.FDwfDigitalOutConfigure(hdwf, fStart);
            return ret;
        }

        public static int FDwfDigitalOutStatus(int hdwf, ref byte psts)
        {
            int ret = PINVOKE.FDwfDigitalOutStatus(hdwf, ref psts);
            return ret;
        }

        public static int FDwfDigitalOutInternalClockInfo(int hdwf, ref double phzFreq)
        {
            int ret = PINVOKE.FDwfDigitalOutInternalClockInfo(hdwf, ref phzFreq);
            return ret;
        }

        public static int FDwfDigitalOutTriggerSourceSet(int hdwf, int trigsrc)
        {
            int ret = PINVOKE.FDwfDigitalOutTriggerSourceSet(hdwf, trigsrc);
            return ret;
        }

        public static int FDwfDigitalOutTriggerSourceGet(int hdwf, ref int ptrigsrc)
        {
            int ret = PINVOKE.FDwfDigitalOutTriggerSourceGet(hdwf, ref ptrigsrc);
            return ret;
        }

        public static int FDwfDigitalOutRunInfo(int hdwf, ref double psecMin, ref double psecMax)
        {
            int ret = PINVOKE.FDwfDigitalOutRunInfo(hdwf, ref psecMin, ref psecMax);
            return ret;
        }

        public static int FDwfDigitalOutRunSet(int hdwf, double secRun)
        {
            int ret = PINVOKE.FDwfDigitalOutRunSet(hdwf, secRun);
            return ret;
        }

        public static int FDwfDigitalOutRunGet(int hdwf, ref double psecRun)
        {
            int ret = PINVOKE.FDwfDigitalOutRunGet(hdwf, ref psecRun);
            return ret;
        }

        public static int FDwfDigitalOutRunStatus(int hdwf, ref double psecRun)
        {
            int ret = PINVOKE.FDwfDigitalOutRunStatus(hdwf, ref psecRun);
            return ret;
        }

        public static int FDwfDigitalOutWaitInfo(int hdwf, ref double psecMin, ref double psecMax)
        {
            int ret = PINVOKE.FDwfDigitalOutWaitInfo(hdwf, ref psecMin, ref psecMax);
            return ret;
        }

        public static int FDwfDigitalOutWaitSet(int hdwf, double secWait)
        {
            int ret = PINVOKE.FDwfDigitalOutWaitSet(hdwf, secWait);
            return ret;
        }

        public static int FDwfDigitalOutWaitGet(int hdwf, ref double psecWait)
        {
            int ret = PINVOKE.FDwfDigitalOutWaitGet(hdwf, ref psecWait);
            return ret;
        }

        public static int FDwfDigitalOutRepeatInfo(int hdwf, ref uint pnMin, ref uint pnMax)
        {
            int ret = PINVOKE.FDwfDigitalOutRepeatInfo(hdwf, ref pnMin, ref pnMax);
            return ret;
        }

        public static int FDwfDigitalOutRepeatSet(int hdwf, uint cRepeat)
        {
            int ret = PINVOKE.FDwfDigitalOutRepeatSet(hdwf, cRepeat);
            return ret;
        }

        public static int FDwfDigitalOutRepeatGet(int hdwf, ref uint pcRepeat)
        {
            int ret = PINVOKE.FDwfDigitalOutRepeatGet(hdwf, ref pcRepeat);
            return ret;
        }

        public static int FDwfDigitalOutRepeatStatus(int hdwf, ref uint pcRepeat)
        {
            int ret = PINVOKE.FDwfDigitalOutRepeatStatus(hdwf, ref pcRepeat);
            return ret;
        }

        public static int FDwfDigitalOutTriggerSlopeSet(int hdwf, int slope)
        {
            int ret = PINVOKE.FDwfDigitalOutTriggerSlopeSet(hdwf, slope);
            return ret;
        }

        public static int FDwfDigitalOutTriggerSlopeGet(int hdwf, ref int pslope)
        {
            int ret = PINVOKE.FDwfDigitalOutTriggerSlopeGet(hdwf, ref pslope);
            return ret;
        }

        public static int FDwfDigitalOutRepeatTriggerSet(int hdwf, int fRepeatTrigger)
        {
            int ret = PINVOKE.FDwfDigitalOutRepeatTriggerSet(hdwf, fRepeatTrigger);
            return ret;
        }

        public static int FDwfDigitalOutRepeatTriggerGet(int hdwf, ref int pfRepeatTrigger)
        {
            int ret = PINVOKE.FDwfDigitalOutRepeatTriggerGet(hdwf, ref pfRepeatTrigger);
            return ret;
        }

        public static int FDwfDigitalOutCount(int hdwf, ref int pcChannel)
        {
            int ret = PINVOKE.FDwfDigitalOutCount(hdwf, ref pcChannel);
            return ret;
        }

        public static int FDwfDigitalOutEnableSet(int hdwf, int idxChannel, int fEnable)
        {
            int ret = PINVOKE.FDwfDigitalOutEnableSet(hdwf, idxChannel, fEnable);
            return ret;
        }

        public static int FDwfDigitalOutEnableGet(int hdwf, int idxChannel, ref int pfEnable)
        {
            int ret = PINVOKE.FDwfDigitalOutEnableGet(hdwf, idxChannel, ref pfEnable);
            return ret;
        }

        public static int FDwfDigitalOutOutputInfo(int hdwf, int idxChannel, ref int pfsDwfDigitalOutOutput)
        {
            int ret = PINVOKE.FDwfDigitalOutOutputInfo(hdwf, idxChannel, ref pfsDwfDigitalOutOutput);
            return ret;
        }

        public static int FDwfDigitalOutOutputSet(int hdwf, int idxChannel, int v)
        {
            int ret = PINVOKE.FDwfDigitalOutOutputSet(hdwf, idxChannel, v);
            return ret;
        }

        public static int FDwfDigitalOutOutputGet(int hdwf, int idxChannel, ref int pv)
        {
            int ret = PINVOKE.FDwfDigitalOutOutputGet(hdwf, idxChannel, ref pv);
            return ret;
        }

        public static int FDwfDigitalOutTypeInfo(int hdwf, int idxChannel, ref int pfsDwfDigitalOutType)
        {
            int ret = PINVOKE.FDwfDigitalOutTypeInfo(hdwf, idxChannel, ref pfsDwfDigitalOutType);
            return ret;
        }

        public static int FDwfDigitalOutTypeSet(int hdwf, int idxChannel, int v)
        {
            int ret = PINVOKE.FDwfDigitalOutTypeSet(hdwf, idxChannel, v);
            return ret;
        }

        public static int FDwfDigitalOutTypeGet(int hdwf, int idxChannel, ref int pv)
        {
            int ret = PINVOKE.FDwfDigitalOutTypeGet(hdwf, idxChannel, ref pv);
            return ret;
        }

        public static int FDwfDigitalOutIdleInfo(int hdwf, int idxChannel, ref int pfsDwfDigitalOutIdle)
        {
            int ret = PINVOKE.FDwfDigitalOutIdleInfo(hdwf, idxChannel, ref pfsDwfDigitalOutIdle);
            return ret;
        }

        public static int FDwfDigitalOutIdleSet(int hdwf, int idxChannel, int v)
        {
            int ret = PINVOKE.FDwfDigitalOutIdleSet(hdwf, idxChannel, v);
            return ret;
        }

        public static int FDwfDigitalOutIdleGet(int hdwf, int idxChannel, ref int pv)
        {
            int ret = PINVOKE.FDwfDigitalOutIdleGet(hdwf, idxChannel, ref pv);
            return ret;
        }

        public static int FDwfDigitalOutDividerInfo(int hdwf, int idxChannel, ref uint vMin, ref uint vMax)
        {
            int ret = PINVOKE.FDwfDigitalOutDividerInfo(hdwf, idxChannel, ref vMin, ref vMax);
            return ret;
        }

        public static int FDwfDigitalOutDividerInitSet(int hdwf, int idxChannel, uint v)
        {
            int ret = PINVOKE.FDwfDigitalOutDividerInitSet(hdwf, idxChannel, v);
            return ret;
        }

        public static int FDwfDigitalOutDividerInitGet(int hdwf, int idxChannel, ref uint pv)
        {
            int ret = PINVOKE.FDwfDigitalOutDividerInitGet(hdwf, idxChannel, ref pv);
            return ret;
        }

        public static int FDwfDigitalOutDividerSet(int hdwf, int idxChannel, uint v)
        {
            int ret = PINVOKE.FDwfDigitalOutDividerSet(hdwf, idxChannel, v);
            return ret;
        }

        public static int FDwfDigitalOutDividerGet(int hdwf, int idxChannel, ref uint pv)
        {
            int ret = PINVOKE.FDwfDigitalOutDividerGet(hdwf, idxChannel, ref pv);
            return ret;
        }

        public static int FDwfDigitalOutCounterInfo(int hdwf, int idxChannel, ref uint vMin, ref uint vMax)
        {
            int ret = PINVOKE.FDwfDigitalOutCounterInfo(hdwf, idxChannel, ref vMin, ref vMax);
            return ret;
        }

        public static int FDwfDigitalOutCounterInitSet(int hdwf, int idxChannel, int fHigh, uint v)
        {
            int ret = PINVOKE.FDwfDigitalOutCounterInitSet(hdwf, idxChannel, fHigh, v);
            return ret;
        }

        public static int FDwfDigitalOutCounterInitGet(int hdwf, int idxChannel, ref int pfHigh, ref uint pv)
        {
            int ret = PINVOKE.FDwfDigitalOutCounterInitGet(hdwf, idxChannel, ref pfHigh, ref pv);
            return ret;
        }

        public static int FDwfDigitalOutCounterSet(int hdwf, int idxChannel, uint vLow, uint vHigh)
        {
            int ret = PINVOKE.FDwfDigitalOutCounterSet(hdwf, idxChannel, vLow, vHigh);
            return ret;
        }

        public static int FDwfDigitalOutCounterGet(int hdwf, int idxChannel, ref uint pvLow, ref uint pvHigh)
        {
            int ret = PINVOKE.FDwfDigitalOutCounterGet(hdwf, idxChannel, ref pvLow, ref pvHigh);
            return ret;
        }

        public static int FDwfDigitalOutDataInfo(int hdwf, int idxChannel, ref uint pcountOfBitsMax)
        {
            int ret = PINVOKE.FDwfDigitalOutDataInfo(hdwf, idxChannel, ref pcountOfBitsMax);
            return ret;
        }

        public static int FDwfDigitalOutDataSet(int hdwf, int idxChannel, byte[] rgBits, uint countOfBits)
        {
            int ret = PINVOKE.FDwfDigitalOutDataSet(hdwf, idxChannel, rgBits, countOfBits);
            return ret;
        }

    }
}
