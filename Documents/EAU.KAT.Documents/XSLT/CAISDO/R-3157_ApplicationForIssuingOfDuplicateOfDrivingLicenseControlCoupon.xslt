<xsl:stylesheet version="1.0" xmlns:AFIDDLCC="http://ereg.egov.bg/segment/R-3157"
                xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
				        xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
				        xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
				        xmlns:P="http://ereg.egov.bg/segment/0009-000008"
				        xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
				        xmlns:ID="http://ereg.egov.bg/segment/0009-000006"
				        xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
				        xmlns:PA="http://ereg.egov.bg/segment/0009-000094"
				        xmlns:AFIDVOD="http://ereg.egov.bg/segment/R-3158"
				        xmlns:PI="http://ereg.egov.bg/segment/R-3015"
				        xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
				        xmlns:DBIF="http://ereg.egov.bg/segment/R-3041"
				        xmlns:IBDIP="http://ereg.egov.bg/segment/R-3033"
				        xmlns:OICIBID="http://ereg.egov.bg/value/R-3034"
				        xmlns:DMST="http://ereg.egov.bg/segment/R-3040"
				        xmlns:SARD="http://ereg.egov.bg/segment/0009-000141"
				        xmlns:EASF="http://ereg.egov.bg/segment/0009-000153"
				        xmlns:E="http://ereg.egov.bg/segment/0009-000013"
                xmlns:DECL="http://ereg.egov.bg/segment//R-3136"
                xmlns:SAWRAM="http://ereg.egov.bg/segment/0009-000152"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:ADD="http://ereg.egov.bg/segment/0009-000139"
                xmlns:xslExtension="urn:XSLExtension"
                
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				
xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

  <xsl:include href="./KATBaseTemplates.xslt"/>

  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="AFIDDLCC:ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCoupon">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>
        <table align="center" cellpadding="5" width= "700px">
          <thead>
            <tr>
              <th>
                &#160;
              </th>
              <th>
                <p align="right">
                  ДО<br/>
                  НАЧАЛНИКА НА "ПЪТНА ПОЛИЦИЯ" - <xsl:choose>
                    <xsl:when test="AFIDDLCC:ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponData/AFIDVOD:PermanentAddress">
                      <xsl:value-of  select="AFIDDLCC:ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponData/AFIDVOD:PermanentAddress/PA:DistrictGRAOName"/>
                    </xsl:when>
                    <xsl:when test="AFIDDLCC:ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponData/AFIDVOD:CurrentAddress">
                      <xsl:value-of  select="AFIDDLCC:ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponData/AFIDVOD:CurrentAddress/PA:DistrictGRAOName"/>
                    </xsl:when>
                  </xsl:choose>
                </p>
              </th>

            </tr>
            <tr>
              <th colspan ="2">
                &#160;
              </th>
            </tr>
            <tr>
              <th colspan ="2">
                <h2>
                  ЗАЯВЛЕНИЕ - ДЕКЛАРАЦИЯ
                </h2>
              </th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <xsl:call-template name="ApplicationElectronicServiceApplicantAndRepresentativeWithAddress">
              <xsl:with-param name="ElectronicServiceApplicant" select = "AFIDDLCC:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant" />
              <xsl:with-param name="PermanentGRAOAddress" select = "AFIDDLCC:ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponData/AFIDVOD:PermanentAddress" />
              <xsl:with-param name="CurrentGRAOAddress" select = "AFIDDLCC:ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponData/AFIDVOD:CurrentAddress" />
            </xsl:call-template>

            <tr>
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <tr>
              <td align="center" colspan ="2">
                <b>ГОСПОДИН НАЧАЛНИК,</b>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                <xsl:choose>
                  <xsl:when test="AFIDDLCC:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1001'">
                    Моля
                  </xsl:when>
                  <xsl:when test="AFIDDLCC:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1002'">
                    Моля, в качеството на пълномощник,
                  </xsl:when>
                  <xsl:when test="AFIDDLCC:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1003'">
                    Моля, в качеството на законен представител,
                  </xsl:when>
                  <xsl:when test="AFIDDLCC:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1004'">
                    Моля, в качеството на длъжностно лице,
                  </xsl:when>
                </xsl:choose>
                да ми бъде издаден дубликат на контролния талон към свидетелството за управление на МПС, тъй като същият е&#160;
                <xsl:choose>
                  <xsl:when test="AFIDDLCC:ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponData/AFIDVOD:CouponDuplicateIssuensReason = '1' ">
                    изгубен.
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDDLCC:ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponData/AFIDVOD:CouponDuplicateIssuensReason = '2' ">
                    откраднат.
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDDLCC:ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponData/AFIDVOD:CouponDuplicateIssuensReason = '3' ">
                    повреден.
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDDLCC:ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponData/AFIDVOD:CouponDuplicateIssuensReason = '4' ">
                    унищожен.
                  </xsl:when>
                </xsl:choose>
              </td>

            </tr>

            
            <xsl:call-template name="AgreementToReceiveERefusal">
              <xsl:with-param name="AgreementToReceiveERefusal" select = "AFIDDLCC:ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponData/AFIDVOD:AgreementToReceiveERefusal" />
            </xsl:call-template>
              
            <xsl:call-template name="Declarations">
              <xsl:with-param name="Declarations" select = "AFIDDLCC:Declarations" />
              <xsl:with-param name="Declaration" select = "AFIDDLCC:Declarations/AFIDDLCC:Declaration" />
            </xsl:call-template>
             
            <xsl:call-template name="ApplicationElectronicAdministrativeServiceFooterLite">
              <xsl:with-param name="ElectronicAdministrativeServiceFooter" select = "AFIDDLCC:ElectronicAdministrativeServiceFooter" />
            </xsl:call-template>

          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>