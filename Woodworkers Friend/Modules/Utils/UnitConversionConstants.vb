' ============================================================================
' Last Updated: January 27, 2026
' Changes: Initial creation - Centralized all unit conversion constants
'          including volume, area, length, weight, and top coat multipliers
' ============================================================================

''' <summary>
''' Contains conversion constants for various unit measurements used throughout the application
''' </summary>
Public Module UnitConversionConstants

    ' Volume Conversions
    Public Const OUNCES_TO_ML As Double = 29.5735 ' 1 fl oz = 29.5735 ml

    Public Const GALLONS_TO_LITERS As Double = 3.78541 ' 1 gal = 3.78541 L
    Public Const QUARTS_TO_LITERS As Double = 0.946353 ' 1 qt = 0.946353 L
    Public Const PINTS_TO_LITERS As Double = 0.473176 ' 1 pt = 0.473176 L

    Public Const GALLONS_TO_OUNCES As Double = 128.0 ' 1 gal = 128 fl oz
    Public Const QUARTS_TO_OUNCES As Double = 32.0 ' 1 qt = 32 fl oz
    Public Const PINTS_TO_OUNCES As Double = 16.0 ' 1 pt = 16 fl oz

    ' Cubic Conversions
    Public Const CUBIC_INCH_TO_FLUID_OUNCES As Double = 0.554113 ' 1 cubic inch ? 0.554113 US fluid ounces

    ' Area Conversions
    Public Const SQ_INCHES_TO_SQ_FEET As Integer = 144 ' 1 sq ft = 144 sq in

    ' Length Conversions
    Public Const INCHES_TO_MM As Double = 25.4 ' 1 inch = 25.4 mm

    Public Const MM_TO_INCHES As Double = 1.0 / 25.4 ' 1 mm = 0.0393701 inches

    ' Weight Conversions
    Public Const KG_TO_LBS As Double = 2.20462 ' 1 kg = 2.20462 lbs

    Public Const LBS_TO_KG As Double = 0.453592 ' 1 lb = 0.453592 kg

    ' Top Coat Constants
    Public Const TOPCOAT_MULTIPLIER As Double = 0.275

    Public Const MATTE_WATER_MULTIPLIER As Double = 0.16
    Public Const GLOSS_WATER_MULTIPLIER As Double = 0.5

End Module
