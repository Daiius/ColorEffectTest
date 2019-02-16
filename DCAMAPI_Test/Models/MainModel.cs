﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;




namespace DCAMAPI_Test.Models
{




    public class MainModel
    {
        [StructLayout(LayoutKind.Sequential)]
        struct DCAM_GUID
        {
            public uint data1;
            public ushort data2;
            public ushort data3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] data4;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct DCAMAPI_INIT
        {
            public int size;
            public int iDeviceCount;
            public int reserved;
            public int initoptionbytes;
            public IntPtr initoption;
            public DCAM_GUID guid;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct DCAMDEV_OPEN
        {
            public int size;
            public int index;
            public IntPtr hdcam;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct DCAMWAIT_OPEN
        {
            public int size;
            public int supportevent;
            public IntPtr hwait;
            public IntPtr hdcam;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct DCAMWAIT_START
        {
            public int size;
            public int eventhappend;
            public int eventmask;
            public int timeout;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct DCAMCAP_TRANSFERINFO
        {
            public int size;
            public int iKind;
            public int nNewestFrameIndex;
            public int nFrameCount;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct DCAMBUF_ATTACH
        {
            public int size;
            public int iKind;
            public IntPtr buffer;

        }

        [DllImport("dcamapi.dll")]
        extern static uint dcamapi_init(ref DCAMAPI_INIT param);

        [DllImport("dcamapi.dll")]
        extern static uint dcamapi_uninit();

        [DllImport("dcamapi.dll")]
        extern static uint dcamdev_open(ref DCAMDEV_OPEN param);

        [DllImport("dcamapi.dll")]
        extern static uint dcamdev_close(IntPtr handle);

        [DllImport("dcamapi.dll")]
        extern static uint dcamprop_getvalue(IntPtr handle, int iProp, ref double pValue);

        [DllImport("dcamapi.dll")]
        extern static uint dcamwait_open(ref DCAMWAIT_OPEN param);

        [DllImport("dcamapi.dll")]
        extern static uint dcambuf_alloc(IntPtr handle, int nbuffer);

        [DllImport("dcamapi.dll")]
        extern static uint dcambuf_release(IntPtr handle);

        [DllImport("dcamapi.dll")]
        extern static uint dcamwait_close(IntPtr hwait);

        [DllImport("dcamapi.dll")]
        extern static uint dcamcap_start(IntPtr handle, int mode);

        enum _DCAMPROPOPTION : long
        {
            /*** direction flag for dcam_getnextpropertyid(), dcam_querypropertyvalue() ***/
            DCAMPROP_OPTION_PRIOR = 0xFF000000, /* prior value		*/
            DCAMPROP_OPTION_NEXT = 0x01000000,  /* next value or id	*/

            /*** direction flag for dcam_querypropertyvalue() ***/
            DCAMPROP_OPTION_NEAREST = 0x80000000,   /* nearest value	*/    /* reserved */

            /*** option for dcam_getnextpropertyid() ***/
            DCAMPROP_OPTION_SUPPORT = 0x00000000,   /* default option */
            DCAMPROP_OPTION_UPDATED = 0x00000001,   /* UPDATED and VOLATILE can be used at same time */
            DCAMPROP_OPTION_VOLATILE = 0x00000002,  /* UPDATED and VOLATILE can be used at same time */
            DCAMPROP_OPTION_ARRAYELEMENT = 0x00000004,  /* ARRAYELEMENT */

            /*** for all option parameter ***/
            DCAMPROP_OPTION_NONE = 0x00000000   /*** no option ***/

        }

        enum _DCAMPROPATTRIBUTE : long
        {
            /* supporting information of DCAM_PROPERTYATTR */
            DCAMPROP_ATTR_HASRANGE = 0x80000000,
            DCAMPROP_ATTR_HASSTEP = 0x40000000,
            DCAMPROP_ATTR_HASDEFAULT = 0x20000000,
            DCAMPROP_ATTR_HASVALUETEXT = 0x10000000,

            /* property id information */
            DCAMPROP_ATTR_HASCHANNEL = 0x08000000,  /* value can set the value for each channels */

            /* property attribute */
            DCAMPROP_ATTR_AUTOROUNDING = 0x00800000,	
		/* The dcam_setproperty() or dcam_setgetproperty() will failure if this bit exists. */
		/* If this flag does not exist, the value will be round up when it is not supported. */
	DCAMPROP_ATTR_STEPPING_INCONSISTENT			
								= 0x00400000,
            /* The valuestep of DCAM_PROPERTYATTR is not consistent across the entire range of	*/
            /* values.																			*/
            DCAMPROP_ATTR_DATASTREAM = 0x00200000,  /* value is releated to image attribute		*/

            DCAMPROP_ATTR_HASRATIO = 0x00100000,    /* value has ratio control capability		*/

            DCAMPROP_ATTR_VOLATILE = 0x00080000,    /* value may be changed by user or automatically */

            DCAMPROP_ATTR_WRITABLE = 0x00020000,    /* value can be set when state is manual	*/
            DCAMPROP_ATTR_READABLE = 0x00010000,    /* value is readable when state is manual	*/

            DCAMPROP_ATTR_HASVIEW = 0x00008000, /* value can set the value for each views	*/
            DCAMPROP_ATTR__SYSTEM = 0x00004000, /* system id								*/    /* reserved */

            DCAMPROP_ATTR_ACCESSREADY = 0x00002000, /* This value can get or set at READY status */
            DCAMPROP_ATTR_ACCESSBUSY = 0x00001000,  /* This value can get or set at BUSY status */

            DCAMPROP_ATTR_ADVANCED = 0x00000800,    /* User has to take care to change this value *//* reserved */
            DCAMPROP_ATTR_ACTION = 0x00000400,  /* writing value takes related effect		*/    /* reserved */
            DCAMPROP_ATTR_EFFECTIVE = 0x00000200,   /* value is effective						*/    /* reserved */

            /* property value type */
            DCAMPROP_TYPE_NONE = 0x00000000,    /* undefined 								*/
            DCAMPROP_TYPE_MODE = 0x00000001,    /* 01:	mode, 32bit integer in case of 32bit OS	*/
            DCAMPROP_TYPE_LONG = 0x00000002,    /* 02:	32bit integer in case of 32bit OS	*/
            DCAMPROP_TYPE_REAL = 0x00000003,    /* 03:	64bit float							*/
                                                /*      no 32bit float						*/

            /* application has to use double-float type variable even the property is not REAL.	*/

            DCAMPROP_TYPE_MASK = 0x0000000F /* mask for property value type				*/
        }

        enum _DCAMPROPATTRIBUTE2
        {
            /* supporting information of DCAM_PROPERTYATTR */
            DCAMPROP_ATTR2_ARRAYBASE = 0x08000000,
            DCAMPROP_ATTR2_ARRAYELEMENT = 0x04000000,

            DCAMPROP_ATTR2_REAL32 = 0x02000000,
            DCAMPROP_ATTR2_INITIALIZEIMPROPER
                                        = 0x00000001,

            DCAMPROP_ATTR2__FUTUREUSE = 0x000FFFFC
        }

        enum _DCAMPROPUNIT
        {
            DCAMPROP_UNIT_SECOND = 1,           /* sec */
            DCAMPROP_UNIT_CELSIUS = 2,          /* for sensor temperature */
            DCAMPROP_UNIT_KELVIN = 3,           /* for color temperature */
            DCAMPROP_UNIT_METERPERSECOND = 4,           /* for LINESPEED */
            DCAMPROP_UNIT_PERSECOND = 5,            /* for FRAMERATE and LINERATE */
            DCAMPROP_UNIT_DEGREE = 6,           /* for OUTPUT ROTATION */    /* reserved */
            DCAMPROP_UNIT_MICROMETER = 7,           /* for length */    /* reserved */

            DCAMPROP_UNIT_NONE = 0
        }

        enum _DCAMPROPMODEVALUE : long
        {
            /* DCAM_IDPROP_SENSORMODE */
            DCAMPROP_SENSORMODE__AREA = 1,          /*	"AREA"					*/
            DCAMPROP_SENSORMODE__SLIT = 2,          /*	"SLIT"					*/    /* reserved */
            DCAMPROP_SENSORMODE__LINE = 3,          /*	"LINE"					*/
            DCAMPROP_SENSORMODE__TDI = 4,           /*	"TDI"					*/
            DCAMPROP_SENSORMODE__FRAMING = 5,           /*	"FRAMING"				*/    /* reserved */
            DCAMPROP_SENSORMODE__PARTIALAREA = 6,           /*	"PARTIAL AREA"			*/    /* reserved */
            DCAMPROP_SENSORMODE__SLITLINE = 9,          /*	"SLIT LINE"				*/    /* reserved */
            DCAMPROP_SENSORMODE__TDI_EXTENDED = 10,         /*	"TDI EXTENDED"			*/
            DCAMPROP_SENSORMODE__PANORAMIC = 11,            /*	"PANORAMIC"				*/    /* reserved */
            DCAMPROP_SENSORMODE__PROGRESSIVE = 12,          /*	"PROGRESSIVE"			*/    /* reserved */
            DCAMPROP_SENSORMODE__SPLITVIEW = 14,            /*	"SPLIT VIEW"			*/    /* reserved */
            DCAMPROP_SENSORMODE__DUALLIGHTSHEET = 16,           /*	"DUAL LIGHTSHEET"		*/

            /* DCAM_IDPROP_READOUTSPEED */
            DCAMPROP_READOUTSPEED__SLOWEST = 1,         /*	no text					*/
            DCAMPROP_READOUTSPEED__FASTEST = 0x7FFFFFFFL,   /*	no text,w/o				*/

            /* DCAM_IDPROP_READOUT_DIRECTION */
            DCAMPROP_READOUT_DIRECTION__FORWARD = 1,            /*	"FORWARD"				*/
            DCAMPROP_READOUT_DIRECTION__BACKWARD = 2,           /*	"BACKWARD"				*/
            DCAMPROP_READOUT_DIRECTION__BYTRIGGER = 3,          /*	"BY TRIGGER"			*/
            DCAMPROP_READOUT_DIRECTION__DIVERGE = 5,            /*	"DIVERGE"				*/

            /* DCAM_IDPROP_READOUT_UNIT */
            /*	DCAMPROP_READOUT_UNIT__LINE					= 1,	*/        /*	"LINE"					*/  /* reserved */
            DCAMPROP_READOUT_UNIT__FRAME = 2,           /*	"FRAME"					*/
            DCAMPROP_READOUT_UNIT__BUNDLEDLINE = 3,         /*	"BUNDLED LINE"			*/
            DCAMPROP_READOUT_UNIT__BUNDLEDFRAME = 4,            /*	"BUNDLED FRAME"			*/

            /* DCAM_IDPROP_CCDMODE */
            DCAMPROP_CCDMODE__NORMALCCD = 1,            /*	"NORMAL CCD"			*/
            DCAMPROP_CCDMODE__EMCCD = 2,            /*	"EM CCD"				*/

            /* DCAM_IDPROP_CMOSMODE */
            DCAMPROP_CMOSMODE__NORMAL = 1,          /*	"NORMAL"				*/
            DCAMPROP_CMOSMODE__NONDESTRUCTIVE = 2,          /*	"NON DESTRUCTIVE"		*/

            /* DCAM_IDPROP_OUTPUT_INTENSITY		 */
            DCAMPROP_OUTPUT_INTENSITY__NORMAL = 1,          /*	"NORMAL"				*/
            DCAMPROP_OUTPUT_INTENSITY__TESTPATTERN = 2,         /*	"TEST PATTERN"			*/

            /* DCAM_IDPROP_OUTPUTDATA_ORIENTATION	 */                                                    /* reserved */
            DCAMPROP_OUTPUTDATA_ORIENTATION__NORMAL = 1,                                            /* reserved */
            DCAMPROP_OUTPUTDATA_ORIENTATION__MIRROR = 2,                                            /* reserved */
            DCAMPROP_OUTPUTDATA_ORIENTATION__FLIP = 3,                                          /* reserved */

            /* DCAM_IDPROP_OUTPUTDATA_OPERATION		*/
            DCAMPROP_OUTPUTDATA_OPERATION__RAW = 1,
            DCAMPROP_OUTPUTDATA_OPERATION__ALIGNED = 2,

            /* DCAM_IDPROP_TESTPATTERN_KIND		 */
            DCAMPROP_TESTPATTERN_KIND__FLAT = 2,            /* "FLAT"					*/
            DCAMPROP_TESTPATTERN_KIND__HORZGRADATION = 4,           /* "HORZGRADATION"			*/
            DCAMPROP_TESTPATTERN_KIND__IHORZGRADATION = 5,          /* "INVERT HORZGRADATION"	*/
            DCAMPROP_TESTPATTERN_KIND__VERTGRADATION = 6,           /* "VERTGRADATION"			*/
            DCAMPROP_TESTPATTERN_KIND__IVERTGRADATION = 7,          /* "INVERT VERTGRADATION"	*/
            DCAMPROP_TESTPATTERN_KIND__LINE = 8,            /* "LINE"					*/
            DCAMPROP_TESTPATTERN_KIND__DIAGONAL = 10,           /* "DIAGONAL"				*/
            DCAMPROP_TESTPATTERN_KIND__FRAMECOUNT = 12,         /* "FRAMECOUNT"				*/

            /* DCAM_IDPROP_DIGITALBINNING_METHOD */
            DCAMPROP_DIGITALBINNING_METHOD__MINIMUM = 1,            /*	"MINIMUM"				*/
            DCAMPROP_DIGITALBINNING_METHOD__MAXIMUM = 2,            /*	"MAXIMUM"				*/
            DCAMPROP_DIGITALBINNING_METHOD__ODD = 3,            /*	"ODD"					*/
            DCAMPROP_DIGITALBINNING_METHOD__EVEN = 4,           /*	"EVEN"					*/
            DCAMPROP_DIGITALBINNING_METHOD__SUM = 5,            /*	"SUM"					*/
            DCAMPROP_DIGITALBINNING_METHOD__AVERAGE = 6,            /*	"AVERAGE"				*/

            /* DCAM_IDPROP_TRIGGERSOURCE */
            DCAMPROP_TRIGGERSOURCE__INTERNAL = 1,           /*	"INTERNAL"				*/
            DCAMPROP_TRIGGERSOURCE__EXTERNAL = 2,           /*	"EXTERNAL"				*/
            DCAMPROP_TRIGGERSOURCE__SOFTWARE = 3,           /*	"SOFTWARE"				*/
            DCAMPROP_TRIGGERSOURCE__MASTERPULSE = 4,            /*	"MASTER PULSE"			*/

            /* DCAM_IDPROP_TRIGGERACTIVE */
            DCAMPROP_TRIGGERACTIVE__EDGE = 1,           /*	"EDGE"					*/
            DCAMPROP_TRIGGERACTIVE__LEVEL = 2,          /*	"LEVEL"					*/
            DCAMPROP_TRIGGERACTIVE__SYNCREADOUT = 3,            /*	"SYNCREADOUT"			*/
            DCAMPROP_TRIGGERACTIVE__POINT = 4,          /*	"POINT"					*/

            /* DCAM_IDPROP_BUS_SPEED */
            DCAMPROP_BUS_SPEED__SLOWEST = 1,            /*	no text					*/
            DCAMPROP_BUS_SPEED__FASTEST = 0x7FFFFFFFL,  /*	no text,w/o				*/

            /* DCAM_IDPROP_TRIGGER_MODE */
            DCAMPROP_TRIGGER_MODE__NORMAL = 1,          /*	"NORMAL"				*/
                                                        /*	= 2,	*/
            DCAMPROP_TRIGGER_MODE__PIV = 3,         /*	"PIV"					*/
            DCAMPROP_TRIGGER_MODE__START = 6,           /*	"START"					*/
            DCAMPROP_TRIGGER_MODE__MULTIGATE = 7,           /*	"MULTIGATE"				*/    /* reserved */
            DCAMPROP_TRIGGER_MODE__MULTIFRAME = 8,          /*	"MULTIFRAME"			*/    /* reserved */

            /* DCAM_IDPROP_TRIGGERPOLARITY */
            DCAMPROP_TRIGGERPOLARITY__NEGATIVE = 1,         /*	"NEGATIVE"				*/
            DCAMPROP_TRIGGERPOLARITY__POSITIVE = 2,         /*	"POSITIVE"				*/

            /* DCAM_IDPROP_TRIGGER_CONNECTOR */
            DCAMPROP_TRIGGER_CONNECTOR__INTERFACE = 1,          /*	"INTERFACE"				*/
            DCAMPROP_TRIGGER_CONNECTOR__BNC = 2,            /*	"BNC"					*/
            DCAMPROP_TRIGGER_CONNECTOR__MULTI = 3,          /*	"MULTI"					*/

            /* DCAM_IDPROP_INTERNALTRIGGER_HANDLING */
            DCAMPROP_INTERNALTRIGGER_HANDLING__SHORTEREXPOSURETIME = 1, /*	"SHORTER EXPOSURE TIME"	*/
            DCAMPROP_INTERNALTRIGGER_HANDLING__FASTERFRAMERATE = 2, /*	"FASTER FRAME RATE"		*/
            DCAMPROP_INTERNALTRIGGER_HANDLING__ABANDONWRONGFRAME = 3,   /*	"ABANDON WRONG FRAME"	*/
            DCAMPROP_INTERNALTRIGGER_HANDLING__BURSTMODE = 4,   /*	"BURST MODE"			*/
            DCAMPROP_INTERNALTRIGGER_HANDLING__INDIVIDUALEXPOSURE = 7,  /*	"INDIVIDUAL EXPOSURE TIME"	*/

            /* DCAM_IDPROP_SYNCREADOUT_SYSTEMBLANK */
            DCAMPROP_SYNCREADOUT_SYSTEMBLANK__STANDARD = 1,         /*	"STANDARD"				*/
            DCAMPROP_SYNCREADOUT_SYSTEMBLANK__MINIMUM = 2,          /*	"MINIMUM"				*/

            /* DCAM_IDPROP_TRIGGERENABLE_ACTIVE */
            DCAMPROP_TRIGGERENABLE_ACTIVE__DENY = 1,            /*	"DENY"					*/
            DCAMPROP_TRIGGERENABLE_ACTIVE__ALWAYS = 2,          /*	"ALWAYS"				*/
            DCAMPROP_TRIGGERENABLE_ACTIVE__LEVEL = 3,           /*	"LEVEL"					*/
            DCAMPROP_TRIGGERENABLE_ACTIVE__START = 4,           /*	"START"					*/

            /* DCAM_IDPROP_TRIGGERENABLE_POLARITY */
            DCAMPROP_TRIGGERENABLE_POLARITY__NEGATIVE = 1,          /*	"NEGATIVE"				*/
            DCAMPROP_TRIGGERENABLE_POLARITY__POSITIVE = 2,          /*	"POSITIVE"				*/
            DCAMPROP_TRIGGERENABLE_POLARITY__INTERLOCK = 3,         /*	"INTERLOCK"				*/

            /* DCAM_IDPROP_OUTPUTTRIGGER_CHANNELSYNC */
            DCAMPROP_OUTPUTTRIGGER_CHANNELSYNC__1CHANNEL = 1,           /*	"1 Channel"				*/
            DCAMPROP_OUTPUTTRIGGER_CHANNELSYNC__2CHANNELS = 2,          /*	"2 Channels"			*/
            DCAMPROP_OUTPUTTRIGGER_CHANNELSYNC__3CHANNELS = 3,          /*	"3 Channels"			*/

            /* DCAM_IDPROP_OUTPUTTRIGGER_PROGRAMABLESTART */
            DCAMPROP_OUTPUTTRIGGER_PROGRAMABLESTART__FIRSTEXPOSURE = 1, /*	"FIRST EXPOSURE"		*/
            DCAMPROP_OUTPUTTRIGGER_PROGRAMABLESTART__FIRSTREADOUT = 2,  /*	"FIRST READOUT"			*/

            /* DCAM_IDPROP_OUTPUTTRIGGER_SOURCE */
            DCAMPROP_OUTPUTTRIGGER_SOURCE__EXPOSURE = 1,            /*	"EXPOSURE"				*/
            DCAMPROP_OUTPUTTRIGGER_SOURCE__READOUTEND = 2,          /*	"READOUT END"			*/
            DCAMPROP_OUTPUTTRIGGER_SOURCE__VSYNC = 3,           /*	"VSYNC"					*/
            DCAMPROP_OUTPUTTRIGGER_SOURCE__HSYNC = 4,           /*	"HSYNC"					*/
            DCAMPROP_OUTPUTTRIGGER_SOURCE__TRIGGER = 6,         /*	"TRIGGER"				*/

            /* DCAM_IDPROP_OUTPUTTRIGGER_POLARITY */
            DCAMPROP_OUTPUTTRIGGER_POLARITY__NEGATIVE = 1,          /*	"NEGATIVE"				*/
            DCAMPROP_OUTPUTTRIGGER_POLARITY__POSITIVE = 2,          /*	"POSITIVE"				*/

            /* DCAM_IDPROP_OUTPUTTRIGGER_ACTIVE */
            DCAMPROP_OUTPUTTRIGGER_ACTIVE__EDGE = 1,            /*	"EDGE"					*/
            DCAMPROP_OUTPUTTRIGGER_ACTIVE__LEVEL = 2,           /*	"LEVEL"					*/
                                                                /*	DCAMPROP_OUTPUTTRIGGER_ACTIVE__PULSE		= 3,	*/        /*	"PULSE"					*/  /* reserved */

            /* DCAM_IDPROP_OUTPUTTRIGGER_KIND */
            DCAMPROP_OUTPUTTRIGGER_KIND__LOW = 1,           /*	"LOW"					*/
            DCAMPROP_OUTPUTTRIGGER_KIND__EXPOSURE = 2,          /*	"EXPOSURE"				*/
            DCAMPROP_OUTPUTTRIGGER_KIND__PROGRAMABLE = 3,           /*	"PROGRAMABLE"			*/
            DCAMPROP_OUTPUTTRIGGER_KIND__TRIGGERREADY = 4,          /*	"TRIGGER READY"			*/
            DCAMPROP_OUTPUTTRIGGER_KIND__HIGH = 5,          /*	"HIGH"					*/

            /* DCAM_IDPROP_OUTPUTTRIGGER_BASESENSOR */
            DCAMPROP_OUTPUTTRIGGER_BASESENSOR__VIEW1 = 1,           /*	"VIEW 1"				*/
            DCAMPROP_OUTPUTTRIGGER_BASESENSOR__VIEW2 = 2,           /*	"VIEW 2"				*/
            DCAMPROP_OUTPUTTRIGGER_BASESENSOR__ANYVIEW = 15,            /*	"ANY VIEW"				*/
            DCAMPROP_OUTPUTTRIGGER_BASESENSOR__ALLVIEWS = 16,           /*	"ALL VIEWS"				*/

            /* DCAM_IDPROP_EXPOSURETIME_CONTROL */
            DCAMPROP_EXPOSURETIME_CONTROL__OFF = 1,         /*	"OFF"					*/
            DCAMPROP_EXPOSURETIME_CONTROL__NORMAL = 2,          /*	"NORMAL"				*/

            /* DCAM_IDPROP_TRIGGER_FIRSTEXPOSURE */
            DCAMPROP_TRIGGER_FIRSTEXPOSURE__NEW = 1,            /*	"NEW"					*/
            DCAMPROP_TRIGGER_FIRSTEXPOSURE__CURRENT = 2,            /*	"CURRENT"				*/

            /* DCAM_IDPROP_TRIGGER_GLOBALEXPOSURE */
            DCAMPROP_TRIGGER_GLOBALEXPOSURE__NONE = 1,          /*	"NONE"				*/
            DCAMPROP_TRIGGER_GLOBALEXPOSURE__ALWAYS = 2,            /*	"ALWAYS"			*/
            DCAMPROP_TRIGGER_GLOBALEXPOSURE__DELAYED = 3,           /*	"DELAYED"			*/
            DCAMPROP_TRIGGER_GLOBALEXPOSURE__EMULATE = 4,           /*	"EMULATE"			*/
            DCAMPROP_TRIGGER_GLOBALEXPOSURE__GLOBALRESET = 5,           /*	"GLOBAL RESET"		*/

            /* DCAM_IDPROP_FIRSTTRIGGER_BEHAVIOR */
            DCAMPROP_FIRSTTRIGGER_BEHAVIOR__STARTEXPOSURE = 1,      /*	"START EXPOSURE"				*/
            DCAMPROP_FIRSTTRIGGER_BEHAVIOR__STARTREADOUT = 2,       /*	"START READOUT"				*/

            /* DCAM_IDPROP_MASTERPULSE_MODE */
            DCAMPROP_MASTERPULSE_MODE__CONTINUOUS = 1,          /*	"CONTINUOUS"		*/
            DCAMPROP_MASTERPULSE_MODE__START = 2,           /*	"START"				*/
            DCAMPROP_MASTERPULSE_MODE__BURST = 3,           /*	"BURST"				*/

            /* DCAM_IDPROP_MASTERPULSE_TRIGGERSOURCE */
            DCAMPROP_MASTERPULSE_TRIGGERSOURCE__EXTERNAL = 1,           /*	"EXTERNAL"		*/
            DCAMPROP_MASTERPULSE_TRIGGERSOURCE__SOFTWARE = 2,           /*	"SOFTWARE"		*/

            /* DCAM_IDPROP_MECHANICALSHUTTER */
            DCAMPROP_MECHANICALSHUTTER__AUTO = 1,           /*	"AUTO"					*/
            DCAMPROP_MECHANICALSHUTTER__CLOSE = 2,          /*	"CLOSE"					*/
            DCAMPROP_MECHANICALSHUTTER__OPEN = 3,           /*	"OPEN"					*/

            /* DCAM_IDPROP_MECHANICALSHUTTER_AUTOMODE */                                                /* reserved */
                                                                                                        /*	DCAMPROP_MECHANICALSHUTTER_AUTOMODE__OPEN_WHEN_EXPOSURE	= 1,*/    /* "OPEN WHEN EXPOSURE"	*/  /* reserved */
                                                                                                                                                                                                            /*	DCAMPROP_MECHANICALSHUTTER_AUTOMODE__CLOSE_WHEN_READOUT	= 2,*/    /* "CLOSE WHEN READOUT"	*/  /* reserved */

            /* DCAM_IDPROP_LIGHTMODE */
            DCAMPROP_LIGHTMODE__LOWLIGHT = 1,           /*	"LOW LIGHT"				*/
            DCAMPROP_LIGHTMODE__HIGHLIGHT = 2,          /*	"HIGH LIGHT"			*/

            /* DCAM_IDPROP_SENSITIVITYMODE */
            DCAMPROP_SENSITIVITYMODE__OFF = 1,          /*	"OFF"					*/
            DCAMPROP_SENSITIVITYMODE__ON = 2,           /*	"ON"					*/
            DCAMPROP_SENSITIVITY2_MODE__INTERLOCK = 3,          /*	"INTERLOCK"				*/

            /* DCAM_IDPROP_EMGAINWARNING_STATUS */
            DCAMPROP_EMGAINWARNING_STATUS__NORMAL = 1,          /*	"NORMAL"				*/
            DCAMPROP_EMGAINWARNING_STATUS__WARNING = 2,         /*	"WARNING"				*/
            DCAMPROP_EMGAINWARNING_STATUS__PROTECTED = 3,           /*	"PROTECTED"				*/

            /* DCAM_IDPROP_PHOTONIMAGINGMODE */
            DCAMPROP_PHOTONIMAGINGMODE__0 = 0,          /*	"0"						*/
            DCAMPROP_PHOTONIMAGINGMODE__1 = 1,          /*	"1"						*/
            DCAMPROP_PHOTONIMAGINGMODE__2 = 2,          /*	"2"						*/
            DCAMPROP_PHOTONIMAGINGMODE__3 = 3,          /*	"2"						*/

            /* DCAM_IDPROP_SENSORCOOLER */
            DCAMPROP_SENSORCOOLER__OFF = 1,         /*	"OFF"					*/
            DCAMPROP_SENSORCOOLER__ON = 2,          /*	"ON"					*/
                                                    /*	DCAMPROP_SENSORCOOLER__BEST					= 3,	*/        /*	"BEST"					*/  /* reserved */
            DCAMPROP_SENSORCOOLER__MAX = 4,         /*	"MAX"					*/

            /* DCAM_IDPROP_SENSORTEMPERATURE_STATUS */
            DCAMPROP_SENSORTEMPERATURE_STATUS__NORMAL = 0,      /*	"NORMAL"				*/
            DCAMPROP_SENSORTEMPERATURE_STATUS__WARNING = 1,     /*	"WARNING"				*/
            DCAMPROP_SENSORTEMPERATURE_STATUS__PROTECTION = 2,      /*	"PROTECTION"			*/

            /* DCAM_IDPROP_SENSORCOOLERSTATUS */
            DCAMPROP_SENSORCOOLERSTATUS__ERROR4 = -4,           /*	"ERROR4"				*/
            DCAMPROP_SENSORCOOLERSTATUS__ERROR3 = -3,           /*	"ERROR3"				*/
            DCAMPROP_SENSORCOOLERSTATUS__ERROR2 = -2,           /*	"ERROR2"				*/
            DCAMPROP_SENSORCOOLERSTATUS__ERROR1 = -1,           /*	"ERROR1"				*/
            DCAMPROP_SENSORCOOLERSTATUS__NONE = 0,          /*	"NONE"					*/
            DCAMPROP_SENSORCOOLERSTATUS__OFF = 1,           /*	"OFF"					*/
            DCAMPROP_SENSORCOOLERSTATUS__READY = 2,         /*	"READY"					*/
            DCAMPROP_SENSORCOOLERSTATUS__BUSY = 3,          /*	"BUSY"					*/
            DCAMPROP_SENSORCOOLERSTATUS__ALWAYS = 4,            /*	"ALWAYS"				*/

            /* DCAM_IDPROP_CONTRAST_CONTROL */                                                            /* reserved */
                                                                                                          /*	DCAMPROP_CONTRAST_CONTROL__OFF				= 1,	*/      /*	"OFF"					*/    /* reserved */
                                                                                                                                                                                                              /*	DCAMPROP_CONTRAST_CONTROL__ON				= 2,	*/      /*	"ON"					*/    /* reserved */
                                                                                                                                                                                                                                                                                                                  /*	DCAMPROP_CONTRAST_CONTROL__FRONTPANEL		= 3,	*/      /*	"FRONT PANEL"			*/    /* reserved */

            /* DCAM_IDPROP_REALTIMEAGAINCORRECT_LEVEL */
            DCAMPROP_REALTIMEGAINCORRECT_LEVEL__1 = 1,          /*	"1"						*/
            DCAMPROP_REALTIMEGAINCORRECT_LEVEL__2 = 2,          /*	"2"						*/
            DCAMPROP_REALTIMEGAINCORRECT_LEVEL__3 = 3,          /*	"3"						*/
            DCAMPROP_REALTIMEGAINCORRECT_LEVEL__4 = 4,          /*	"4"						*/
            DCAMPROP_REALTIMEGAINCORRECT_LEVEL__5 = 5,          /*	"5"						*/

            /* DCAM_IDPROP_WHITEBALANCEMODE */
            DCAMPROP_WHITEBALANCEMODE__FLAT = 1,            /*	"FLAT"					*/
            DCAMPROP_WHITEBALANCEMODE__AUTO = 2,            /*	"AUTO"					*/
            DCAMPROP_WHITEBALANCEMODE__TEMPERATURE = 3,         /*	"TEMPERATURE"			*/
            DCAMPROP_WHITEBALANCEMODE__USERPRESET = 4,          /*	"USER PRESET"			*/

            /* DCAM_IDPROP_DARKCALIB_TARGET */
            DCAMPROP_DARKCALIB_TARGET__ALL = 1,         /*	"ALL"					*/
            DCAMPROP_DARKCALIB_TARGET__ANALOG = 2,          /*	"ANALOG"				*/

            /* DCAM_IDPROP_SHADINGCALIB_METHOD */
            DCAMPROP_SHADINGCALIB_METHOD__AVERAGE = 1,          /*	"AVERAGE"				*/
            DCAMPROP_SHADINGCALIB_METHOD__MAXIMUM = 2,          /*	"MAXIMUM"				*/
            DCAMPROP_SHADINGCALIB_METHOD__USETARGET = 3,            /*	"USE TARGET"			*/

            /* DCAM_IDPROP_CAPTUREMODE */
            DCAMPROP_CAPTUREMODE__NORMAL = 1,           /*	"NORMAL"				*/
            DCAMPROP_CAPTUREMODE__DARKCALIB = 2,            /*	"DARK CALIBRATION"		*/
            DCAMPROP_CAPTUREMODE__SHADINGCALIB = 3,         /*	"SHADING CALIBRATION"	*/
            DCAMPROP_CAPTUREMODE__TAPGAINCALIB = 4,         /*	"TAP GAIN CALIBRATION"	*/
            DCAMPROP_CAPTUREMODE__BACKFOCUSCALIB = 5,           /*	"BACK FOCUS CALIBRATION"*/    /* ORCA-D2 */

            /* DCAM_IDPROP_INTERFRAMEALU_ENABLE */
            DCAMPROP_INTERFRAMEALU_ENABLE__OFF = 1,         /*	"OFF"					*/
            DCAMPROP_INTERFRAMEALU_ENABLE__TRIGGERSOURCE_ALL = 2,       /*	"TRIGGER SOURCE ALL"	*/
            DCAMPROP_INTERFRAMEALU_ENABLE__TRIGGERSOURCE_INTERNAL = 3,  /*	"TRIGGER SOURCE INTERNAL ONLY"	*/

            /* DCAM_IDPROP_SUBTRACT_DATASTATUS/DCAM_IDPROP_SHADINGCALIB_DATASTATUS */
            DCAMPROP_CALIBDATASTATUS__NONE = 1,         /*	"NONE"					*/
            DCAMPROP_CALIBDATASTATUS__FORWARD = 2,          /*	"FORWARD"				*/
            DCAMPROP_CALIBDATASTATUS__BACKWARD = 3,         /*	"BACKWARD"				*/
            DCAMPROP_CALIBDATASTATUS__BOTH = 4,         /*	"BOTH"					*/

            /* DCAM_IDPROP_TAPGAINCALIB_METHOD */
            DCAMPROP_TAPGAINCALIB_METHOD__AVE = 1,          /*	"AVERAGE"				*/
            DCAMPROP_TAPGAINCALIB_METHOD__MAX = 2,          /*	"MAXIMUM"				*/
            DCAMPROP_TAPGAINCALIB_METHOD__MIN = 3,          /*	"MINIMUM"				*/

            /* DCAM_IDPROP_RECURSIVEFILTERFRAMES */
            DCAMPROP_RECURSIVEFILTERFRAMES__2 = 2,          /*	"2 FRAMES"				*/
            DCAMPROP_RECURSIVEFILTERFRAMES__4 = 4,          /*	"4 FRAMES"				*/
            DCAMPROP_RECURSIVEFILTERFRAMES__8 = 8,          /*	"8 FRAMES"				*/
            DCAMPROP_RECURSIVEFILTERFRAMES__16 = 16,            /*	"16 FRAMES"				*/
            DCAMPROP_RECURSIVEFILTERFRAMES__32 = 32,            /*	"32 FRAMES"				*/
            DCAMPROP_RECURSIVEFILTERFRAMES__64 = 64,            /*	"64 FRAMES"				*/

            /* DCAM_IDPROP_INTENSITYLUT_MODE */
            DCAMPROP_INTENSITYLUT_MODE__THROUGH = 1,            /*	"THROUGH"				*/
            DCAMPROP_INTENSITYLUT_MODE__PAGE = 2,           /*	"PAGE"					*/
            DCAMPROP_INTENSITYLUT_MODE__CLIP = 3,           /*	"CLIP"					*/

            /* DCAM_IDPROP_BINNING */
            DCAMPROP_BINNING__1 = 1,            /*	"1X1"					*/
            DCAMPROP_BINNING__2 = 2,            /*	"2X2"					*/
            DCAMPROP_BINNING__4 = 4,            /*	"4X4"					*/
            DCAMPROP_BINNING__8 = 8,            /*	"8X8"					*/
            DCAMPROP_BINNING__16 = 16,          /*	"16X16"					*/

            /* DCAM_IDPROP_COLORTYPE */
            DCAMPROP_COLORTYPE__BW = 0x00000001,    /*	"BW"					*/
            DCAMPROP_COLORTYPE__RGB = 0x00000002,   /*	"RGB"					*/
            DCAMPROP_COLORTYPE__BGR = 0x00000003,   /*	"BGR"					*/
                                                    /* other values are resereved */

            /* DCAM_IDPROP_BITSPERCHANNEL */
            DCAMPROP_BITSPERCHANNEL__8 = 8,         /*	"8BIT"					*/
            DCAMPROP_BITSPERCHANNEL__10 = 10,           /*	"10BIT"					*/
            DCAMPROP_BITSPERCHANNEL__12 = 12,           /*	"12BIT"					*/
            DCAMPROP_BITSPERCHANNEL__14 = 14,           /*	"14BIT"					*/
            DCAMPROP_BITSPERCHANNEL__16 = 16,           /*	"16BIT"					*/

            /* DCAM_IDPROP_IMAGEFOOTER_FORMAT */

            /* DCAM_IDPROP_DEFECTCORRECT_MODE */
            DCAMPROP_DEFECTCORRECT_MODE__OFF = 1,           /*	"OFF"					*/
            DCAMPROP_DEFECTCORRECT_MODE__ON = 2,            /*	"ON"					*/

            /* DCAM_IDPROP_DEFECTCORRECT_METHOD */
            DCAMPROP_DEFECTCORRECT_METHOD__CEILING = 3,         /*	"CEILING"				*/
            DCAMPROP_DEFECTCORRECT_METHOD__PREVIOUS = 4,            /*	"PREVIOUS"				*/

            /* DCAM_IDPROP_HOTPIXELCORRECT_LEVEL */
            DCAMPROP_HOTPIXELCORRECT_LEVEL__STANDARD = 1,           /*	"STANDARD"				*/
            DCAMPROP_HOTPIXELCORRECT_LEVEL__MINIMUM = 2,            /*	"MINIMUM"				*/
            DCAMPROP_HOTPIXELCORRECT_LEVEL__AGGRESSIVE = 3,         /*	"AGGRESSIVE"			*/

            /* DCAM_IDPROP_SYSTEM_ALIVE */
            DCAMPROP_SYSTEM_ALIVE__OFFLINE = 1,         /*	"OFFLINE"				*/
            DCAMPROP_SYSTEM_ALIVE__ONLINE = 2,          /*	"ONLINE"				*/

            /* DCAM_IDPROP_TIMESTAMP_MODE */
            DCAMPROP_TIMESTAMP_MODE__NONE = 1,          /*	"NONE"					*/
            DCAMPROP_TIMESTAMP_MODE__LINEBEFORELEFT = 2,            /*	"LINE BEFORE LEFT"		*/
            DCAMPROP_TIMESTAMP_MODE__LINEOVERWRITELEFT = 3,         /*	"LINE OVERWRITE LEFT"	*/
            DCAMPROP_TIMESTAMP_MODE__AREABEFORELEFT = 4,            /*	"AREA BEFORE LEFT"		*/
            DCAMPROP_TIMESTAMP_MODE__AREAOVERWRITELEFT = 5,         /*	"AREA OVERWRITE LEFT"	*/

            /* DCAM_IDPROP_PACECONTROL_MODE */
            DCAMPROP_PACECONTROL_MODE__OFF = 1,         /* "OFF"					*/
            DCAMPROP_PACECONTROL_MODE__INTERVAL = 2,            /* "INTERVAL"				*/
            DCAMPROP_PACECONTROL_MODE__THINNING = 3,            /* "THINNING"				*/

            /* DCAM_IDPROP_TIMING_EXPOSURE */
            DCAMPROP_TIMING_EXPOSURE__AFTERREADOUT = 1,         /*	"AFTER READOUT"			*/
            DCAMPROP_TIMING_EXPOSURE__OVERLAPREADOUT = 2,           /*	"OVERLAP READOUT"		*/
            DCAMPROP_TIMING_EXPOSURE__ROLLING = 3,          /*	"ROLLING"				*/
            DCAMPROP_TIMING_EXPOSURE__ALWAYS = 4,           /*	"ALWAYS"				*/
            DCAMPROP_TIMING_EXPOSURE__TDI = 5,          /*	"TDI"					*/

            /* DCAM_IDPROP_TIMESTAMP_PRODUCER */
            DCAMPROP_TIMESTAMP_PRODUCER__NONE = 1,      /* "NONE"					*/
            DCAMPROP_TIMESTAMP_PRODUCER__DCAMMODULE = 2,        /* "DCAM MODULE"			*/
            DCAMPROP_TIMESTAMP_PRODUCER__KERNELDRIVER = 3,      /* "KERNEL DRIVER"			*/
            DCAMPROP_TIMESTAMP_PRODUCER__CAPTUREDEVICE = 4,     /* "CAPTURE DEVICE"			*/
            DCAMPROP_TIMESTAMP_PRODUCER__IMAGINGDEVICE = 5,     /* "IMAGING DEVICE"			*/

            /* DCAM_IDPROP_FRAMESTAMP_PRODUCER */
            DCAMPROP_FRAMESTAMP_PRODUCER__NONE = 1,     /* "NONE"					*/
            DCAMPROP_FRAMESTAMP_PRODUCER__DCAMMODULE = 2,       /* "DCAM MODULE"			*/
            DCAMPROP_FRAMESTAMP_PRODUCER__KERNELDRIVER = 3,     /* "KERNEL DRIVER"			*/
            DCAMPROP_FRAMESTAMP_PRODUCER__CAPTUREDEVICE = 4,        /* "CAPTURE DEVICE"			*/
            DCAMPROP_FRAMESTAMP_PRODUCER__IMAGINGDEVICE = 5,        /* "IMAGING DEVICE"			*/

            /* DCAM_IDPROP_CAMERASTATUS_INTENSITY */
            DCAMPROP_CAMERASTATUS_INTENSITY__GOOD = 1,  /* "GOOD"					*/
            DCAMPROP_CAMERASTATUS_INTENSITY__TOODARK = 2,   /* "TOO DRAK"				*/
            DCAMPROP_CAMERASTATUS_INTENSITY__TOOBRIGHT = 3, /* "TOO BRIGHT"				*/
            DCAMPROP_CAMERASTATUS_INTENSITY__UNCARE = 4,    /* "UNCARE"					*/
            DCAMPROP_CAMERASTATUS_INTENSITY__EMGAIN_PROTECTION = 5, /* "EMGAIN PROTECTION"		*/
            DCAMPROP_CAMERASTATUS_INTENSITY__INCONSISTENT_OPTICS = 6,   /* "INCONSISTENT OPTICS"	*/
            DCAMPROP_CAMERASTATUS_INTENSITY__NODATA = 7,    /* "NO DATA"				*/

            /* DCAM_IDPROP_CAMERASTATUS_INPUTTRIGGER */
            DCAMPROP_CAMERASTATUS_INPUTTRIGGER__GOOD = 1,   /* "GOOD"					*/
            DCAMPROP_CAMERASTATUS_INPUTTRIGGER__NONE = 2,   /* "NONE"					*/
            DCAMPROP_CAMERASTATUS_INPUTTRIGGER__TOOFREQUENT = 3,    /* "TOO FREQUENT"			*/

            /* DCAM_IDPROP_CAMERASTATUS_CALIBRATION */
            DCAMPROP_CAMERASTATUS_CALIBRATION__DONE = 1,/* "DONE"					*/
            DCAMPROP_CAMERASTATUS_CALIBRATION__NOTYET = 2,/* "NOT YET"				*/
            DCAMPROP_CAMERASTATUS_CALIBRATION__NOTRIGGER = 3,/* "NO TRIGGER"				*/
            DCAMPROP_CAMERASTATUS_CALIBRATION__TOOFREQUENTTRIGGER = 4,/* "TOO FREQUENT TRIGGER"	*/
            DCAMPROP_CAMERASTATUS_CALIBRATION__OUTOFADJUSTABLERANGE = 5,/* "OUT OF ADJUSTABLE RANGE"*/
            DCAMPROP_CAMERASTATUS_CALIBRATION__UNSUITABLETABLE = 6,/* "UNSUITABLE TABLE"		*/
            DCAMPROP_CAMERASTATUS_CALIBRATION__TOODARK = 7,/* "TOO DARK"				*/
            DCAMPROP_CAMERASTATUS_CALIBRATION__TOOBRIGHT = 8,/* "TOO BRIGHT"				*/
            DCAMPROP_CAMERASTATUS_CALIBRATION__NOTDETECTOBJECT = 9,/* "NOT DETECT OBJECT"		*/

            /*-- for general purpose --*/
            DCAMPROP_MODE__OFF = 1,         /*	"OFF"					*/
            DCAMPROP_MODE__ON = 2,          /*	"ON"					*/

            /*-- options --*/

            /* for backward compativilities */

            DCAMPROP_SCAN_MODE__NORMAL = DCAMPROP_SENSORMODE__AREA,
            DCAMPROP_SCAN_MODE__SLIT = DCAMPROP_SENSORMODE__SLIT,

            DCAMPROP_SWITCHMODE_OFF = DCAMPROP_MODE__OFF,   /*	"OFF"					*/
            DCAMPROP_SWITCHMODE_ON = DCAMPROP_MODE__ON, /*	"ON"					*/

            DCAMPROP_TRIGGERACTIVE__PULSE = DCAMPROP_TRIGGERACTIVE__SYNCREADOUT,        /*	was "PULSE"	*/

            DCAMPROP_READOUT_DIRECTION__NORMAL = DCAMPROP_READOUT_DIRECTION__FORWARD,           /* VALUETEXT was "NORMAL"	*/
            DCAMPROP_READOUT_DIRECTION__REVERSE = DCAMPROP_READOUT_DIRECTION__BACKWARD,         /* VALUETEXT was "REVERSE"	*/

            /*-- miss spelling --*/
            DCAMPROP_TRIGGERSOURCE__EXERNAL = DCAMPROP_TRIGGERSOURCE__EXTERNAL
        };

        enum _DCAMIDPROP : long
        {
            /*	  0x00000000 - 0x00100000, reserved						*/

            /* Group: TIMING */
            DCAM_IDPROP_TRIGGERSOURCE = 0x00100110, /* R/W, mode,	"TRIGGER SOURCE"		*/
            DCAM_IDPROP_TRIGGERACTIVE = 0x00100120, /* R/W, mode,	"TRIGGER ACTIVE"		*/
            DCAM_IDPROP_TRIGGER_MODE = 0x00100210,  /* R/W, mode,	"TRIGGER MODE"			*/
            DCAM_IDPROP_TRIGGERPOLARITY = 0x00100220,   /* R/W, mode,	"TRIGGER POLARITY"		*/
            DCAM_IDPROP_TRIGGER_CONNECTOR = 0x00100230, /* R/W, mode,	"TRIGGER CONNECTOR"		*/
            DCAM_IDPROP_TRIGGERTIMES = 0x00100240,  /* R/W, long,	"TRIGGER TIMES"			*/
                                                    /*	  0x00100250 is reserved */
            DCAM_IDPROP_TRIGGERDELAY = 0x00100260,  /* R/W, sec,	"TRIGGER DELAY"			*/
            DCAM_IDPROP_INTERNALTRIGGER_HANDLING = 0x00100270,  /* R/W, mode,	"INTERNAL TRIGGER HANDLING"*/
            DCAM_IDPROP_TRIGGERMULTIFRAME_COUNT = 0x00100280,   /* R/W, long,	"TRIGGER MULTI FRAME COUNT"*/
            DCAM_IDPROP_SYNCREADOUT_SYSTEMBLANK = 0x00100290,   /* R/W, mode,	"SYNC READOUT SYSTEM BLANK" */

            DCAM_IDPROP_TRIGGERENABLE_ACTIVE = 0x00100410,  /* R/W, mode,	"TRIGGER ENABLE ACTIVE"	*/
            DCAM_IDPROP_TRIGGERENABLE_POLARITY = 0x00100420,    /* R/W, mode,	"TRIGGER ENABLE POLARITY"*/

            DCAM_IDPROP_TRIGGERNUMBER_FORFIRSTIMAGE = 0x00100810,   /* R/O, long,	"TRIGGER NUMBER FOR FIRST IMAGE" */
            DCAM_IDPROP_TRIGGERNUMBER_FORNEXTIMAGE = 0x00100820,    /* R/O, long,	"TRIGGER NUMBER FOR NEXT IMAGE" */

            DCAM_IDPROP_BUS_SPEED = 0x00180110, /* R/W, long,	"BUS SPEED"				*/

            DCAM_IDPROP_NUMBEROF_OUTPUTTRIGGERCONNECTOR = 0x001C0010,   /* R/O, long,	"NUMBER OF OUTPUT TRIGGER CONNECTOR"*/
            DCAM_IDPROP_OUTPUTTRIGGER_CHANNELSYNC = 0x001C0030, /* R/W, mode,	"OUTPUT TRIGGER CHANNEL SYNC"	*/
            DCAM_IDPROP_OUTPUTTRIGGER_PROGRAMABLESTART = 0x001C0050,    /* R/W, mode,	"OUTPUT TRIGGER PROGRAMABLE START"	*/
            DCAM_IDPROP_OUTPUTTRIGGER_SOURCE = 0x001C0110,  /* R/W, mode,	"OUTPUT TRIGGER SOURCE"		*/
            DCAM_IDPROP_OUTPUTTRIGGER_POLARITY = 0x001C0120,    /* R/W, mode,	"OUTPUT TRIGGER POLARITY"	*/
            DCAM_IDPROP_OUTPUTTRIGGER_ACTIVE = 0x001C0130,  /* R/W, mode,	"OUTPUT TRIGGER ACTIVE"		*/
            DCAM_IDPROP_OUTPUTTRIGGER_DELAY = 0x001C0140,   /* R/W, sec,	"OUTPUT TRIGGER DELAY"		*/
            DCAM_IDPROP_OUTPUTTRIGGER_PERIOD = 0x001C0150,  /* R/W, sec,	"OUTPUT TRIGGER PERIOD"		*/
            DCAM_IDPROP_OUTPUTTRIGGER_KIND = 0x001C0160,    /* R/W, mode,	"OUTPUT TRIGGER KIND"		*/
            DCAM_IDPROP_OUTPUTTRIGGER_BASESENSOR = 0x001C0170,  /* R/W, mode,	"OUTPUT TRIGGER BASE SENSOR" */
            DCAM_IDPROP_OUTPUTTRIGGER_PREHSYNCCOUNT = 0x001C0190,   /* R/W, mode,	"OUTPUT TRIGGER PRE HSYNC COUNT" */
                                                                    /*				 - 0x001C10FF for 16 output trigger connector, reserved		*/
            DCAM_IDPROP__OUTPUTTRIGGER = 0x00000100,    /* the offset of ID for Nth OUTPUT TRIGGER parameter */

            DCAM_IDPROP_MASTERPULSE_MODE = 0x001E0020,  /* R/W, mode,	"MASTER PULSE MODE"			*/
            DCAM_IDPROP_MASTERPULSE_TRIGGERSOURCE = 0x001E0030, /* R/W, mode,	"MASTER PULSE TRIGGER SOURCE"	*/
            DCAM_IDPROP_MASTERPULSE_INTERVAL = 0x001E0040,  /* R/W, sec,	"MASTER PULSE INTERVAL"		*/
            DCAM_IDPROP_MASTERPULSE_BURSTTIMES = 0x001E0050,    /* R/W, long,	"MASTER PULSE BURST TIMES"	*/

            /* Group: FEATURE */
            /* exposure period */
            DCAM_IDPROP_EXPOSURETIME = 0x001F0110,  /* R/W, sec,	"EXPOSURE TIME"			*/
            DCAM_IDPROP_SYNC_MULTIVIEWEXPOSURE = 0x001F0120,    /* R/W, mode,	"SYNCHRONOUS MULTI VIEW EXPOSURE"	*/
            DCAM_IDPROP_EXPOSURETIME_CONTROL = 0x001F0130,  /* R/W, mode,	"EXPOSURE TIME CONTROL"	*/
            DCAM_IDPROP_TRIGGER_FIRSTEXPOSURE = 0x001F0200, /* R/W, mode,	"TRIGGER FIRST EXPOSURE"	*/
            DCAM_IDPROP_TRIGGER_GLOBALEXPOSURE = 0x001F0300,    /* R/W, mode,	"TRIGGER GLOBAL EXPOSURE"	*/
            DCAM_IDPROP_FIRSTTRIGGER_BEHAVIOR = 0x001F0310, /* R/W, mode,	"FIRST TRIGGER BEHAVIOR"	*/
            DCAM_IDPROP_MULTIFRAME_EXPOSURE = 0x001F1000,   /* R/W, sec,	"MULTI FRAME EXPOSURE TIME"	*/
                                                            /*					 - 0x001F1FFF for 256 MULTI FRAME */
            DCAM_IDPROP__MULTIFRAME = 0x00000010,   /* the offset of ID for Nth MULTIFRAME */

            /* anti-blooming */
            DCAM_IDPROP_LIGHTMODE = 0x00200110, /* R/W, mode,	"LIGHT MODE"			*/
                                                /*	  0x00200120 is reserved */

            /* sensitivity */
            DCAM_IDPROP_SENSITIVITYMODE = 0x00200210,   /* R/W, mode,	"SENSITIVITY MODE"		*/
            DCAM_IDPROP_SENSITIVITY = 0x00200220,   /* R/W, long,	"SENSITIVITY"			*/
            DCAM_IDPROP_SENSITIVITY2_MODE = 0x00200230, /* R/W, mode,	"SENSITIVITY2 MODE"		*/    /* reserved */
            DCAM_IDPROP_SENSITIVITY2 = 0x00200240,  /* R/W, long,	"SENSITIVITY2"			*/

            DCAM_IDPROP_DIRECTEMGAIN_MODE = 0x00200250, /* R/W, mode,	"DIRECT EM GAIN MODE"	*/
            DCAM_IDPROP_EMGAINWARNING_STATUS = 0x00200260,  /* R/O, mode,	"EM GAIN WARNING STATUS"*/
            DCAM_IDPROP_EMGAINWARNING_LEVEL = 0x00200270,   /* R/W, long,	"EM GAIN WARNING LEVEL"	*/
            DCAM_IDPROP_EMGAINWARNING_ALARM = 0x00200280,   /* R/W, mode,	"EM GAIN WARNING ALARM"	*/
            DCAM_IDPROP_EMGAINPROTECT_MODE = 0x00200290,    /* R/W, mode,	"EM GAIN PROTECT MODE"	*/
            DCAM_IDPROP_EMGAINPROTECT_AFTERFRAMES = 0x002002A0, /* R/W, long,	"EM GAIN PROTECT AFTER FRAMES"	*/

            DCAM_IDPROP_MEASURED_SENSITIVITY = 0x002002B0,  /* R/O, real,	"MEASURED SENSITIVITY"	*/

            DCAM_IDPROP_PHOTONIMAGINGMODE = 0x002002F0, /* R/W, mode,	"PHOTON IMAGING MODE"	*/

            /* sensor cooler */
            DCAM_IDPROP_SENSORTEMPERATURE = 0x00200310, /* R/O, celsius,"SENSOR TEMPERATURE"	*/
            DCAM_IDPROP_SENSORCOOLER = 0x00200320,  /* R/W, mode,	"SENSOR COOLER"			*/
            DCAM_IDPROP_SENSORTEMPERATURETARGET = 0x00200330,   /* R/W, celsius,"SENSOR TEMPERATURE TARGET"	*/
            DCAM_IDPROP_SENSORCOOLERSTATUS = 0x00200340,    /* R/O, mode,	"SENSOR COOLER STATUS"	*/
            DCAM_IDPROP_SENSORCOOLERFAN = 0x00200350,   /* R/W, mode,	"SENSOR COOLER FAN"		*/
            DCAM_IDPROP_SENSORTEMPERATURE_AVE = 0x00200360, /* R/O, celsius,"SENSOR TEMPERATURE AVE"	*/
            DCAM_IDPROP_SENSORTEMPERATURE_MIN = 0x00200370, /* R/O, celsius,"SENSOR TEMPERATURE MIN"	*/
            DCAM_IDPROP_SENSORTEMPERATURE_MAX = 0x00200380, /* R/O, celsius,"SENSOR TEMPERATURE MAX"	*/
            DCAM_IDPROP_SENSORTEMPERATURE_STATUS = 0x00200390,  /* R/O, mode,	"SENSOR TEMPERATURE STATUS"	*/
            DCAM_IDPROP_SENSORTEMPERATURE_PROTECT = 0x00200400, /* R/W, mode,	"SENSOR TEMPERATURE MODE"	*/

            /* mechanical shutter */
            DCAM_IDPROP_MECHANICALSHUTTER = 0x00200410, /* R/W, mode,	"MECHANICAL SHUTTER"	*/
                                                        /*	DCAM_IDPROP_MECHANICALSHUTTER_AUTOMODE		= 0x00200420,*/    /* R/W, mode,	"MECHANICAL SHUTTER AUTOMODE"	*/ /* reserved */

            /* contrast enhance */
            /*	DCAM_IDPROP_CONTRAST_CONTROL				= 0x00300110,*/    /* R/W, mode,	"CONTRAST CONTROL"		*/ /* reserved */
            DCAM_IDPROP_CONTRASTGAIN = 0x00300120,  /* R/W, long,	"CONTRAST GAIN"			*/
            DCAM_IDPROP_CONTRASTOFFSET = 0x00300130,    /* R/W, long,	"CONTRAST OFFSET"		*/
                                                        /*	  0x00300140 is reserved */
            DCAM_IDPROP_HIGHDYNAMICRANGE_MODE = 0x00300150, /* R/W, mode,	"HIGH DYNAMIC RANGE MODE"	*/
            DCAM_IDPROP_DIRECTGAIN_MODE = 0x00300160,   /* R/W, mode,	"DIRECT GAIN MODE"		*/

            DCAM_IDPROP_REALTIMEGAINCORRECT_MODE = 0x00300170,  /* R/W,	mode,	"REALTIME GAIN CORRECT MODE"	*/
            DCAM_IDPROP_REALTIMEGAINCORRECT_LEVEL = 0x00300180, /* R/W,	mode,	"REALTIME GAIN CORRECT LEVEL"		*/
            DCAM_IDPROP_REALTIMEGAINCORRECT_INTERVAL = 0x00300190,  /* R/W,	mode,	"REALTIME GAIN CORRECT INTERVAL"	*/

            /* color features */
            DCAM_IDPROP_VIVIDCOLOR = 0x00300200,    /* R/W, mode,	"VIVID COLOR"			*/    /* comment */
            DCAM_IDPROP_WHITEBALANCEMODE = 0x00300210,  /* R/W, mode,	"WHITEBALANCE MODE"		*/
            DCAM_IDPROP_WHITEBALANCETEMPERATURE = 0x00300220,   /* R/W, color-temp., "WHITEBALANCE TEMPERATURE"	*/
            DCAM_IDPROP_WHITEBALANCEUSERPRESET = 0x00300230,    /* R/W, long,	"WHITEBALANCE USER PRESET"		*/
                                                                /*	  0x00300310 is reserved */

            /* Group: ALU */
            /* ALU */
            DCAM_IDPROP_INTERFRAMEALU_ENABLE = 0x00380010,  /* R/W, mode,	"INTERFRAME ALU ENABLE"	*/
            DCAM_IDPROP_RECURSIVEFILTER = 0x00380110,   /* R/W, mode,	"RECURSIVE FILTER"		*/
            DCAM_IDPROP_RECURSIVEFILTERFRAMES = 0x00380120, /* R/W, long,	"RECURSIVE FILTER FRAMES"*/
            DCAM_IDPROP_SPOTNOISEREDUCER = 0x00380130,  /* R/W, mode,	"SPOT NOISE REDUCER"	*/
            DCAM_IDPROP_SUBTRACT = 0x00380210,  /* R/W, mode,	"SUBTRACT"				*/
            DCAM_IDPROP_SUBTRACTIMAGEMEMORY = 0x00380220,   /* R/W, mode,	"SUBTRACT IMAGE MEMORY"	*/
            DCAM_IDPROP_STORESUBTRACTIMAGETOMEMORY = 0x00380230,    /* W/O, mode,	"STORE SUBTRACT IMAGE TO MEMORY"	*/
            DCAM_IDPROP_SUBTRACTOFFSET = 0x00380240,    /* R/W, long	"SUBTRACT OFFSET"		*/
            DCAM_IDPROP_DARKCALIB_STABLEMAXINTENSITY = 0x00380250,  /* R/W, long,	"DARKCALIB STABLE MAX INTENSITY"	*/
            DCAM_IDPROP_SUBTRACT_DATASTATUS = 0x003802F0,   /* R/W	mode,	"SUBTRACT DATA STATUS"	*/
            DCAM_IDPROP_SHADINGCALIB_DATASTATUS = 0x00380300,   /* R/W	mode,	"SHADING CALIB DATA STATUS"	*/
            DCAM_IDPROP_SHADINGCORRECTION = 0x00380310, /* R/W, mode,	"SHADING CORRECTION"	*/
            DCAM_IDPROP_SHADINGCALIBDATAMEMORY = 0x00380320,    /* R/W, mode,	"SHADING CALIB DATA MEMORY"		*/
            DCAM_IDPROP_STORESHADINGCALIBDATATOMEMORY = 0x00380330, /* W/O, mode,	"STORE SHADING DATA TO MEMORY"	*/
            DCAM_IDPROP_SHADINGCALIB_METHOD = 0x00380340,   /* R/W, mode,	"SHADING CALIB METHOD"	*/
            DCAM_IDPROP_SHADINGCALIB_TARGET = 0x00380350,   /* R/W, long,	"SHADING CALIB TARGET"	*/
            DCAM_IDPROP_SHADINGCALIB_STABLEMININTENSITY = 0x00380360,   /* R/W, long,	"SHADING CALIB STABLE MIN INTENSITY"	*/
            DCAM_IDPROP_SHADINGCALIB_SAMPLES = 0x00380370,  /* R/W, long,	"SHADING CALIB SAMPLES"	*/
            DCAM_IDPROP_SHADINGCALIB_STABLESAMPLES = 0x00380380,    /* R/W, long,	"SHADING CALIB STABLE SAMPLES"	*/
            DCAM_IDPROP_SHADINGCALIB_STABLEMAXERRORPERCENT = 0x00380390,    /* R/W, long,	"SHADING CALIB STABLE MAX ERROR PERCENT" */
            DCAM_IDPROP_FRAMEAVERAGINGMODE = 0x003803A0,    /* R/W, mode,	"FRAME AVERAGING MODE"		*/
            DCAM_IDPROP_FRAMEAVERAGINGFRAMES = 0x003803B0,  /* R/W, long,	"FRAME AVERAGING FRAMES"*/
            DCAM_IDPROP_DARKCALIB_STABLESAMPLES = 0x003803C0,   /* R/W, long,	"DARKCALIB STABLE SAMPLES"	*/
            DCAM_IDPROP_DARKCALIB_SAMPLES = 0x003803D0, /* R/W, long,	"DARKCALIB SAMPLES"	*/
            DCAM_IDPROP_DARKCALIB_TARGET = 0x003803E0,  /* R/W, long,	"DARKCALIB TARGET" */
            DCAM_IDPROP_CAPTUREMODE = 0x00380410,   /* R/W, mode,	"CAPTURE MODE"			*/
            DCAM_IDPROP_INTENSITYLUT_MODE = 0x00380510, /* R/W, mode,	"INTENSITY LUT MODE"	*/
            DCAM_IDPROP_INTENSITYLUT_PAGE = 0x00380520, /* R/W, long,	"INTENSITY LUT PAGE"	*/
            DCAM_IDPROP_INTENSITYLUT_WHITECLIP = 0x00380530,    /* R/W, long,	"INTENSITY LUT WHITE CLIP"	*/
            DCAM_IDPROP_INTENSITYLUT_BLACKCLIP = 0x00380540,    /* R/W, long,	"INTENSITY LUT BLACK CLIP"	*/
            DCAM_IDPROP_SENSORGAPCORRECT_MODE = 0x00380620, /* R/W,	long,	"SENSOR GAP CORRECT MODE"	*/

            /* TAP CALIBRATION */
            DCAM_IDPROP_TAPGAINCALIB_METHOD = 0x00380F10,   /* R/W, mode,	"TAP GAIN CALIB METHOD"	*/
            DCAM_IDPROP_TAPCALIB_BASEDATAMEMORY = 0x00380F20,   /* R/W, mode,	"TAP CALIB BASE DATA MEMORY"*/
            DCAM_IDPROP_STORETAPCALIBDATATOMEMORY = 0x00380F30, /* W/O, mode,	"STORE TAP CALIB DATA TO MEMORY"*/
            DCAM_IDPROP_TAPCALIBDATAMEMORY = 0x00380F40,    /* W/O, mode,	"TAP CALIB DATA MEMORY"	*/
            DCAM_IDPROP_NUMBEROF_TAPCALIB = 0x00380FF0, /* R/W, long,	"NUMBER OF TAP CALIB"	*/
            DCAM_IDPROP_TAPCALIB_GAIN = 0x00381000, /* R/W, mode,	"TAP CALIB GAIN"		*/
            DCAM_IDPROP__TAPCALIB = 0x00000010, /* the offset of ID for Nth TAPCALIB	*/

            /* Group: READOUT */
            /* readout speed */
            DCAM_IDPROP_READOUTSPEED = 0x00400110,  /* R/W, long,	"READOUT SPEED" 		*/
                                                    /*	  0x00400120 is reserved */
            DCAM_IDPROP_READOUT_DIRECTION = 0x00400130, /* R/W, mode,	"READOUT DIRECTION"		*/
            DCAM_IDPROP_READOUT_UNIT = 0x00400140,  /* R/O, mode,	"READOUT UNIT"			*/

            /* sensor mode */
            DCAM_IDPROP_SENSORMODE = 0x00400210,    /* R/W, mode,	"SENSOR MODE"			*/
            DCAM_IDPROP_SENSORMODE_SLITHEIGHT = 0x00400220, /* R/W, long,	"SENSOR MODE SLIT HEIGHT"		*/    /* reserved */
            DCAM_IDPROP_SENSORMODE_LINEBUNDLEHEIGHT = 0x00400250,   /* R/W, long,	"SENSOR MODE LINE BUNDLEHEIGHT"	*/
            DCAM_IDPROP_SENSORMODE_FRAMINGHEIGHT = 0x00400260,  /* R/W, long,	"SENSOR MODE FRAMING HEIGHT"	*/    /* reserved */
            DCAM_IDPROP_SENSORMODE_PANORAMICSTARTV = 0x00400280,    /* R/W, long,	"SENSOR MODE PANORAMIC START V"	*/

            /* other readout mode */
            DCAM_IDPROP_CCDMODE = 0x00400310,   /* R/W, mode,	"CCD MODE"				*/
            DCAM_IDPROP_EMCCD_CALIBRATIONMODE = 0x00400320, /* R/W, mode,	"EM CCD CALIBRATION MODE"	*/
            DCAM_IDPROP_CMOSMODE = 0x00400350,  /* R/W, mode,	"CMOS MODE"				*/

            /* output mode */
            DCAM_IDPROP_OUTPUT_INTENSITY = 0x00400410,  /* R/W, mode,	"OUTPUT INTENSITY"		*/
            DCAM_IDPROP_OUTPUTDATA_ORIENTATION = 0x00400420,    /* R/W, mode,	"OUTPUT DATA ORIENTATION"	*/    /* reserved */
            DCAM_IDPROP_OUTPUTDATA_ROTATION = 0x00400430,   /* R/W, degree,	"OUTPUT DATA ROTATION"		*/    /* reserved */
            DCAM_IDPROP_OUTPUTDATA_OPERATION = 0x00400440,  /* R/W, mode,	"OUTPUT DATA OPERATION"	*/

            DCAM_IDPROP_TESTPATTERN_KIND = 0x00400510,  /* R/W, mode,	"TEST PATTERN KIND"		*/
            DCAM_IDPROP_TESTPATTERN_OPTION = 0x00400520,    /* R/W, long,	"TEST PATTERN OPTION"	*/

            DCAM_IDPROP_EXTRACTION_MODE = 0x00400620,   /* R/W	mode,	"EXTRACTION MODE	"*/

            /* Group: ROI */
            /* binning and subarray */
            DCAM_IDPROP_BINNING = 0x00401110,   /* R/W, mode,	"BINNING"				*/
            DCAM_IDPROP_BINNING_INDEPENDENT = 0x00401120,   /* R/W, mode,	"BINNING INDEPENDENT"	*/
            DCAM_IDPROP_BINNING_HORZ = 0x00401130,  /* R/W, long,	"BINNING HORZ"			*/
            DCAM_IDPROP_BINNING_VERT = 0x00401140,  /* R/W, long,	"BINNING VERT"			*/
            DCAM_IDPROP_SUBARRAYHPOS = 0x00402110,  /* R/W, long,	"SUBARRAY HPOS"			*/
            DCAM_IDPROP_SUBARRAYHSIZE = 0x00402120, /* R/W, long,	"SUBARRAY HSIZE"		*/
            DCAM_IDPROP_SUBARRAYVPOS = 0x00402130,  /* R/W, long,	"SUBARRAY VPOS"			*/
            DCAM_IDPROP_SUBARRAYVSIZE = 0x00402140, /* R/W, long,	"SUBARRAY VSIZE"		*/
            DCAM_IDPROP_SUBARRAYMODE = 0x00402150,  /* R/W, mode,	"SUBARRAY MODE"			*/
            DCAM_IDPROP_DIGITALBINNING_METHOD = 0x00402160, /* R/W, mode,	"DIGITALBINNING METHOD"	*/
            DCAM_IDPROP_DIGITALBINNING_HORZ = 0x00402170,   /* R/W, long,	"DIGITALBINNING HORZ"	*/
            DCAM_IDPROP_DIGITALBINNING_VERT = 0x00402180,   /* R/W, long,	"DIGITALBINNING VERT"	*/

            /* Group: TIMING */
            /* synchronous timing */
            DCAM_IDPROP_TIMING_READOUTTIME = 0x00403010,    /* R/O, sec,	"TIMING READOUT TIME"			*/
            DCAM_IDPROP_TIMING_CYCLICTRIGGERPERIOD = 0x00403020,    /* R/O, sec,	"TIMING CYCLIC TRIGGER PERIOD"	*/
            DCAM_IDPROP_TIMING_MINTRIGGERBLANKING = 0x00403030, /* R/O, sec,	"TIMING MINIMUM TRIGGER BLANKING"	*/
                                                                /*	  0x00403040 is reserved */
            DCAM_IDPROP_TIMING_MINTRIGGERINTERVAL = 0x00403050, /* R/O, sec,	"TIMING MINIMUM TRIGGER INTERVAL"	*/
            DCAM_IDPROP_TIMING_EXPOSURE = 0x00403060,   /* R/O, mode,	"TIMING EXPOSURE"			*/
            DCAM_IDPROP_TIMING_INVALIDEXPOSUREPERIOD = 0x00403070,  /* R/O, sec,	"INVALID EXPOSURE PERIOD"	*/
            DCAM_IDPROP_TIMING_FRAMESKIPNUMBER = 0x00403080,    /* R/W, long,	"TIMING FRAME SKIP NUMBER"	*/
            DCAM_IDPROP_TIMING_GLOBALEXPOSUREDELAY = 0x00403090,    /* R/O, sec,	"TIMING GLOBAL EXPOSURE DELAY"	*/

            DCAM_IDPROP_INTERNALFRAMERATE = 0x00403810, /* R/W, 1/sec,	"INTERNAL FRAME RATE"		*/
            DCAM_IDPROP_INTERNAL_FRAMEINTERVAL = 0x00403820,    /* R/W, sec,	"INTERNAL FRAME INTERVAL"	*/
            DCAM_IDPROP_INTERNALLINERATE = 0x00403830,  /* R/W, 1/sec,	"INTERNAL LINE RATE"		*/
            DCAM_IDPROP_INTERNALLINESPEED = 0x00403840, /* R/W, m/sec,	"INTERNAL LINE SPEEED"		*/
            DCAM_IDPROP_INTERNAL_LINEINTERVAL = 0x00403850, /* R/W, sec,	"INTERNAL LINE INTERVAL"	*/

            /* system information */

            DCAM_IDPROP_TIMESTAMP_PRODUCER = 0x00410A10,    /* R/O, mode,	"TIME STAMP PRODUCER"	*/
            DCAM_IDPROP_FRAMESTAMP_PRODUCER = 0x00410A20,   /* R/O, mode,	"FRAME STAMP PRODUCER"	*/

            /* Group: READOUT */

            /* image information */
            /*	  0x00420110 is reserved */
            DCAM_IDPROP_COLORTYPE = 0x00420120, /* R/W, mode,	"COLORTYPE"				*/
            DCAM_IDPROP_BITSPERCHANNEL = 0x00420130,    /* R/W, long,	"BIT PER CHANNEL"		*/
                                                        /*	  0x00420140 is reserved */
                                                        /*	  0x00420150 is reserved */

            DCAM_IDPROP_NUMBEROF_CHANNEL = 0x00420180,  /* R/O, long,	"NUMBER OF CHANNEL"		*/
            DCAM_IDPROP_ACTIVE_CHANNELINDEX = 0x00420190,   /* R/W, mode,	"ACTIVE CHANNEL INDEX"	*/
            DCAM_IDPROP_NUMBEROF_VIEW = 0x004201C0, /* R/O, long,	"NUMBER OF VIEW"		*/
            DCAM_IDPROP_ACTIVE_VIEWINDEX = 0x004201D0,  /* R/W, mode,	"ACTIVE VIEW INDEX"		*/

            DCAM_IDPROP_IMAGE_WIDTH = 0x00420210,   /* R/O, long,	"IMAGE WIDTH"			*/
            DCAM_IDPROP_IMAGE_HEIGHT = 0x00420220,  /* R/O, long,	"IMAGE HEIGHT"			*/
            DCAM_IDPROP_IMAGE_ROWBYTES = 0x00420230,    /* R/O, long,	"IMAGE ROWBYTES"		*/
            DCAM_IDPROP_IMAGE_FRAMEBYTES = 0x00420240,  /* R/O, long,	"IMAGE FRAMEBYTES"		*/
            DCAM_IDPROP_IMAGE_TOPOFFSETBYTES = 0x00420250,  /* R/O, long,	"IMAGE TOP OFFSET BYTES"*/        /* reserved */
            DCAM_IDPROP_IMAGE_PIXELTYPE = 0x00420270,   /* R/W, DCAM_PIXELTYPE,	"IMAGE PIXEL TYPE"	*/
            DCAM_IDPROP_IMAGE_CAMERASTAMP = 0x00420300, /* R/W, long,	"IMAGE CAMERA STAMP"	*/

            DCAM_IDPROP_BUFFER_ROWBYTES = 0x00420330,   /* R/O, long,	"BUFFER ROWBYTES"		*/
            DCAM_IDPROP_BUFFER_FRAMEBYTES = 0x00420340, /* R/O, long,	"BUFFER FRAME BYTES"		*/
            DCAM_IDPROP_BUFFER_TOPOFFSETBYTES = 0x00420350, /* R/O, long,	"BUFFER TOP OFFSET BYTES"	*/
            DCAM_IDPROP_BUFFER_PIXELTYPE = 0x00420360,  /* R/O, DCAM_PIXELTYPE,	"BUFFER PIXEL TYPE"	*/

            DCAM_IDPROP_RECORDFIXEDBYTES_PERFILE = 0x00420410,  /* R/O,	long	"RECORD FIXED BYTES PER FILE"	*/
            DCAM_IDPROP_RECORDFIXEDBYTES_PERSESSION = 0x00420420,   /* R/O,	long	"RECORD FIXED BYTES PER SESSION"*/
            DCAM_IDPROP_RECORDFIXEDBYTES_PERFRAME = 0x00420430, /* R/O,	long	"RECORD FIXED BYTES PER FRAME"	*/

            DCAM_IDPROP_IMAGEDETECTOR_PIXELWIDTH = 0x00420810,  /* R/O, micro-meter, "IMAGE DETECTOR PIXEL WIDTH"	*/    /* reserved */
            DCAM_IDPROP_IMAGEDETECTOR_PIXELHEIGHT = 0x00420820, /* R/O, micro-meter, "IMAGE DETECTOR PIXEL HEIGHT"	*/    /* reserved */

            /* frame bundle */
            DCAM_IDPROP_FRAMEBUNDLE_MODE = 0x00421010,  /* R/W, mode,	"FRAMEBUNDLE MODE"		*/
            DCAM_IDPROP_FRAMEBUNDLE_NUMBER = 0x00421020,    /* R/W, long,	"FRAMEBUNDLE NUMBER"	*/
            DCAM_IDPROP_FRAMEBUNDLE_ROWBYTES = 0x00421030,  /* R/O,	long,	"FRAMEBUNDLE ROWBYTES"	*/
            DCAM_IDPROP_FRAMEBUNDLE_FRAMESTEPBYTES = 0x00421040,    /* R/O, long,	"FRAMEBUNDLE FRAME STEP BYTES"	*/

            /* partial area */
            DCAM_IDPROP_NUMBEROF_PARTIALAREA = 0x00430010,  /* R/W, long,	"NUMBER OF PARTIAL AREA"*/
            DCAM_IDPROP_PARTIALAREA_HPOS = 0x00431000,  /* R/W, long,	"PARTIAL AREA HPOS"		*/
            DCAM_IDPROP_PARTIALAREA_HSIZE = 0x00432000, /* R/W, long,	"PARTIAL AREA HSIZE"	*/
            DCAM_IDPROP_PARTIALAREA_VPOS = 0x00433000,  /* R/W, long,	"PARTIAL AREA VPOS"		*/
            DCAM_IDPROP_PARTIALAREA_VSIZE = 0x00434000, /* R/W, long,	"PARTIAL AREA VSIZE"	*/
            DCAM_IDPROP__PARTIALAREA = 0x00000010,  /* the offset of ID for Nth PARTIAL AREA */

            /* multi line */
            DCAM_IDPROP_NUMBEROF_MULTILINE = 0x0044F010,    /* R/W, long,	"NUMBER OF MULTI LINE"	*/
            DCAM_IDPROP_MULTILINE_VPOS = 0x00450000,    /* R/W, long,	"MULTI LINE VPOS"		*/
            DCAM_IDPROP_MULTILINE_VSIZE = 0x00460000,   /* R/W, long,	"MULTI LINE VSIZE"		*/
                                                        /*				 - 0x0046FFFF for 4096 MULTI LINEs			*/        /* reserved */
            DCAM_IDPROP__MULTILINE = 0x00000010,    /* the offset of ID for Nth MULTI LINE */

            /* defect */
            DCAM_IDPROP_DEFECTCORRECT_MODE = 0x00470010,    /* R/W, mode,	"DEFECT CORRECT MODE"	*/
            DCAM_IDPROP_NUMBEROF_DEFECTCORRECT = 0x00470020,    /* R/W, long,	"NUMBER OF DEFECT CORRECT"	*/
            DCAM_IDPROP_HOTPIXELCORRECT_LEVEL = 0x00470030, /* R/W, mode,	"HOT PIXEL CORRECT LEVEL"	*/
            DCAM_IDPROP_DEFECTCORRECT_HPOS = 0x00471000,    /* R/W, long,	"DEFECT CORRECT HPOS"		*/
            DCAM_IDPROP_DEFECTCORRECT_METHOD = 0x00473000,  /* R/W, mode,	"DEFECT CORRECT METHOD"		*/
                                                            /*				 - 0x0047FFFF for 256 DEFECT */
            DCAM_IDPROP__DEFECTCORRECT = 0x00000010,    /* the offset of ID for Nth DEFECT */

            /* Group: PACE CONTROL */
            DCAM_IDPROP_PACECONTROL_MODE = 0x004A0110,  /* R/W, mode,	"PACE CONTROL MODE"		*/
            DCAM_IDPROP_NUMBEROF_PACECONTROL = 0x004A0120,  /* R/W, long,	"NUMBER OF PACE CONTROL"*/
            DCAM_IDPROP_PACECONTROL_COUNT = 0x004A1000, /* R/W, long,	"PACE CONTROL COUNT"	*/
            DCAM_IDPROP_PACECONTROL_INTERVAL = 0x004A2000,  /* R/W, real,	"PACE CONTROL INTERVAL"	*/
                                                            /*				 - 0x004AFFFF for 256 DEFECT, reserved		*/
            DCAM_IDPROP__PACECONTROL = 0x00000010,  /* the offset of ID for Nth PACECONTROL	*/

            /* Group: CALIBREGION */
            DCAM_IDPROP_CALIBREGION_MODE = 0x00402410,  /* R/W, mode,	"CALIBRATE REGION MODE"		*/
            DCAM_IDPROP_NUMBEROF_CALIBREGION = 0x00402420,  /* R/W, long,	"NUMBER OF CALIBRATE REGION"*/
            DCAM_IDPROP_CALIBREGION_HPOS = 0x004B0000,  /* R/W, long,	"CALIBRATE REGION HPOS"		*/
            DCAM_IDPROP_CALIBREGION_HSIZE = 0x004B1000, /* R/W, long,	"CALIBRATE REGION HSIZE"	*/
                                                        /*				 - 0x0048FFFF for 256 REGIONs at least		*/
            DCAM_IDPROP__CALIBREGION = 0x00000010,  /* the offset of ID for Nth REGION		*/

            /* Group: MASKREGION */
            DCAM_IDPROP_MASKREGION_MODE = 0x00402510,   /* R/W, mode,	"MASK REGION MODE"		*/
            DCAM_IDPROP_NUMBEROF_MASKREGION = 0x00402520,   /* R/W, long,	"NUMBER OF MASK REGION"	*/
            DCAM_IDPROP_MASKREGION_HPOS = 0x004C0000,   /* R/W, long,	"MASK REGION HPOS"		*/
            DCAM_IDPROP_MASKREGION_HSIZE = 0x004C1000,  /* R/W, long,	"MASK REGION HSIZE"		*/
                                                        /*				 - 0x0048FFFF for 256 REGIONs at least		*/
            DCAM_IDPROP__MASKREGION = 0x00000010,   /* the offset of ID for Nth REGION		*/

            /* Group: Camera Status */
            DCAM_IDPROP_CAMERASTATUS_INTENSITY = 0x004D1110,    /* R/O, mode,	"CAMERASTATUS INTENSITY"	*/
            DCAM_IDPROP_CAMERASTATUS_INPUTTRIGGER = 0x004D1120, /* R/O, mode,	"CAMERASTATUS INPUT TRIGGER"*/
            DCAM_IDPROP_CAMERASTATUS_CALIBRATION = 0x004D1130,  /* R/O, mode,	"CAMERASTATUS CALIBRATION"	*/

            /* Group: Back Focus Position */
            DCAM_IDPROP_BACKFOCUSPOS_TARGET = 0x00804010,   /* R/W, micro-meter,"BACK FOCUS POSITION TARGET"	*/
            DCAM_IDPROP_BACKFOCUSPOS_CURRENT = 0x00804020,  /* R/O, micro-meter,"BACK FOCUS POSITION CURRENT"	*/
            DCAM_IDPROP_BACKFOCUSPOS_LOADFROMMEMORY = 0x00804050,   /* R/W, long, "BACK FOCUS POSITION LOAD FROM MEMORY"*/
            DCAM_IDPROP_BACKFOCUSPOS_STORETOMEMORY = 0x00804060,    /* W/O, long, "BACK FOCUS POSITION STORE TO MEMORY"	*/

            /* Group: SYSTEM */
            /* system property */

            DCAM_IDPROP_SYSTEM_ALIVE = 0x00FF0010,  /* R/O, mode,	"SYSTEM ALIVE"			*/

            DCAM_IDPROP_CONVERSIONFACTOR_COEFF = 0x00FFE010,    /* R/O, double,	"CONVERSION FACTOR COEFF"	*/
            DCAM_IDPROP_CONVERSIONFACTOR_OFFSET = 0x00FFE020,   /* R/O, double,	"CONVERSION FACTOR OFFSET"	*/

            /*-- options --*/

            /* option */
            DCAM_IDPROP__RATIO = 0x80000000,
            DCAM_IDPROP_EXPOSURETIME_RATIO = DCAM_IDPROP__RATIO | DCAM_IDPROP_EXPOSURETIME,                     /* reserved */
                                                                                                                /* R/W, real,	"EXPOSURE TIME RATIO"	*/                /* reserved */
            DCAM_IDPROP_CONTRASTGAIN_RATIO = DCAM_IDPROP__RATIO | DCAM_IDPROP_CONTRASTGAIN,                     /* reserved */
                                                                                                                /* R/W, real,	"CONTRAST GAIN RATIO"	*/                /* reserved */

            DCAM_IDPROP__CHANNEL = 0x00000001,
            DCAM_IDPROP__VIEW = 0x01000000,

            DCAM_IDPROP__MASK_CHANNEL = 0x0000000F,
            DCAM_IDPROP__MASK_VIEW = 0x0F000000,
            DCAM_IDPROP__MASK_BODY = 0x00FFFFF0,

            /* for backward compativilities */
            DCAMPROP_ATTR_REMOTE_VALUE = _DCAMPROPATTRIBUTE.DCAMPROP_ATTR_VOLATILE,

            DCAMPROP_PHOTONIMAGING_MODE__0 = _DCAMPROPMODEVALUE.DCAMPROP_PHOTONIMAGINGMODE__0,
            DCAMPROP_PHOTONIMAGING_MODE__1 = _DCAMPROPMODEVALUE.DCAMPROP_PHOTONIMAGINGMODE__1,
            DCAMPROP_PHOTONIMAGING_MODE__2 = _DCAMPROPMODEVALUE.DCAMPROP_PHOTONIMAGINGMODE__2,

            DCAM_IDPROP_SCAN_MODE = DCAM_IDPROP_SENSORMODE,
            DCAM_IDPROP_SLITSCAN_HEIGHT = DCAM_IDPROP_SENSORMODE_SLITHEIGHT,

            DCAM_IDPROP_FRAME_BUNDLEMODE = DCAM_IDPROP_FRAMEBUNDLE_MODE,
            DCAM_IDPROP_FRAME_BUNDLENUMBER = DCAM_IDPROP_FRAMEBUNDLE_NUMBER,
            DCAM_IDPROP_FRAME_BUNDLEROWBYTES = DCAM_IDPROP_FRAMEBUNDLE_ROWBYTES,

            DCAM_IDPROP_ACTIVE_VIEW = DCAM_IDPROP_ACTIVE_VIEWINDEX,                     /* reserved */
            DCAM_IDPROP_ACTIVE_VIEWINDEXES = DCAM_IDPROP_ACTIVE_VIEWINDEX,                      /* reserved */
            DCAM_IDPROP_SYNCMULTIVIEWREADOUT = DCAM_IDPROP_SYNC_MULTIVIEWEXPOSURE,              /* reserved */
                                                                                                /*	DCAM_IDPROP_SYNC_FRAMEREADOUTTIME=DCAM_IDPROP_TIMING_READOUTTIME,				*/    /* reserved */
                                                                                                                                                                                          /*	DCAM_IDPROP_SYNC_CYCLICTRIGGERPERIOD = DCAM_IDPROP_TIMING_CYCLICTRIGGERPERIOD,	*/  /* reserved */
            DCAM_IDPROP_SYNC_MINTRIGGERBLANKING = DCAM_IDPROP_TIMING_MINTRIGGERBLANKING,
            DCAM_IDPROP_SYNC_FRAMEINTERVAL = DCAM_IDPROP_INTERNAL_FRAMEINTERVAL,
            DCAM_IDPROP_LOWLIGHTSENSITIVITY = DCAM_IDPROP_PHOTONIMAGINGMODE,

            DCAM_IDPROP_DARKCALIB_MAXIMUMINTENSITY = DCAM_IDPROP_DARKCALIB_STABLEMAXINTENSITY,
            DCAM_IDPROP_SUBTRACT_SAMPLINGCOUNT = DCAM_IDPROP_DARKCALIB_SAMPLES,

            DCAM_IDPROP_SHADINGCALIB_MINIMUMINTENSITY = DCAM_IDPROP_SHADINGCALIB_STABLEMININTENSITY,
            DCAM_IDPROP_SHADINGCALIB_STABLEFRAMECOUNT = DCAM_IDPROP_SHADINGCALIB_STABLESAMPLES,
            DCAM_IDPROP_SHADINGCALIB_INTENSITYMAXIMUMERRORPERCENTAGE = DCAM_IDPROP_SHADINGCALIB_STABLEMAXERRORPERCENT,
            DCAM_IDPROP_SHADINGCALIB_AVERAGEFRAMECOUNT = DCAM_IDPROP_SHADINGCALIB_SAMPLES,

            _end_of_dcam_idprop = 0
        };


        enum DCAM_PIXELTYPE
        {
            DCAM_PIXELTYPE_MONO8 = 0x00000001,
            DCAM_PIXELTYPE_MONO16 = 0x00000002,
            DCAM_PIXELTYPE_MONO12 = 0x00000003,

            DCAM_PIXELTYPE_RGB24 = 0x00000021,
            DCAM_PIXELTYPE_RGB48 = 0x00000022,
            DCAM_PIXELTYPE_BGR24 = 0x00000029,
            DCAM_PIXELTYPE_BGR48 = 0x0000002a,

            DCAM_PIXELTYPE_NONE = 0x00000000
        }

        enum DCAMCAP_START
        {
            DCAMCAP_START_SEQUENCE = -1,
            DCAMCAP_START_SNAP = 0
        }

        public int ImageWidth { get; set; }

        public int ImageHeight { get; set; }

        public void Init()
        {
            var initParam = new DCAMAPI_INIT();
            initParam.size = Marshal.SizeOf(initParam);
            uint result = dcamapi_init(ref initParam);

            var devOpenParam = new DCAMDEV_OPEN();
            devOpenParam.size = Marshal.SizeOf(devOpenParam);
            devOpenParam.index = 0;

            result = dcamdev_open(ref devOpenParam);

            if (result == 1)
            {
                double v = 0.0;
                result = dcamprop_getvalue(devOpenParam.hdcam, (int)_DCAMIDPROP.DCAM_IDPROP_IMAGE_PIXELTYPE, ref v);

                if ((int)v == (int)DCAM_PIXELTYPE.DCAM_PIXELTYPE_MONO12)
                {
                    Console.WriteLine("OK!");
                }
            }

            if (result == 1)
            {
                double v = 0.0;
                result = dcamprop_getvalue(devOpenParam.hdcam, (int)_DCAMIDPROP.DCAM_IDPROP_IMAGE_WIDTH, ref v);
                ImageWidth = (int)v;
            }

            if (result == 1)
            {
                double v = 0.0;
                result = dcamprop_getvalue(devOpenParam.hdcam, (int)_DCAMIDPROP.DCAM_IDPROP_IMAGE_HEIGHT, ref v);
                ImageHeight = (int)v;
            }

            IntPtr hwait;
            if (result == 1)
            {
                var waitOpenParam = new DCAMWAIT_OPEN();
                waitOpenParam.size = Marshal.SizeOf(waitOpenParam);
                waitOpenParam.hdcam = devOpenParam.hdcam;

                result = dcamwait_open(ref waitOpenParam);
                if (result == 1) hwait = waitOpenParam.hwait;
            }

            // NOTE :
            // 画像取得用のバッファは、DCAMAPIが確保する場合とこちらで確保する場合と
            // 2通り使う事が出来る。

            const int nbuffer = 10;
            if (result == 1)
            {
                result = dcambuf_alloc(devOpenParam.hdcam, nbuffer);
            }

            if (result == 1)
            {
                result = dcamcap_start(devOpenParam.hdcam, (int)DCAMCAP_START.DCAMCAP_START_SEQUENCE);
            }

            if (result == 1)
            {
                result = dcamdev_close(devOpenParam.hdcam);
            }

            return;
        }

        public void Uninit()
        {
            uint result = dcamapi_uninit();
        }
    }
}
