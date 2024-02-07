<xsl:stylesheet version="1.0" xmlns:APP="http://ereg.egov.bg/segment/R-3324"
                xmlns:HEADER="http://ereg.egov.bg/segment/R-3031"
                xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
                xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
                xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
                xmlns:P="http://ereg.egov.bg/segment/0009-000008"
                xmlns:AFIOTRPTMD="http://ereg.egov.bg/segment/R-3325"
                xmlns:E="http://ereg.egov.bg/segment/0009-000013"
                xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
                xmlns:PI="http://ereg.egov.bg/segment/0009-000006"
                xmlns:MD="http://ereg.egov.bg/segment/R-3326"
                xmlns:EA="http://ereg.egov.bg/segment/R-3203"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

  <xsl:include href="./KATBaseTemplates.xslt"/>
  
  <xsl:output omit-xml-declaration="yes" method="html"/>

  <xsl:template match="APP:ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchants">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>
        <table align="center" cellpadding="5" width= "700px">
          <thead>
            <tr>
              <th align="center" colspan="2">
                <h2>МИНИСТЕРСТВО НА ВЪТРЕШНИТЕ РАБОТИ</h2>
              </th>
            </tr>
            <tr>
              <th colspan ="2">
                ГЛАВНА ДИРЕКЦИЯ НАЦИОНАЛНА ПОЛИЦИЯ
              </th>
            </tr>
            <tr>
              <th>&#160;</th>
              <th>
                <p align="left">
                  ДО<br/>
                  НАЧАЛНИКА НА<br/>
                  ОТДЕЛ "ПЪТНА ПОЛИЦИЯ"<br/>
                  ПРИ ГДНП<br/>
                  ЧРЕЗ: НАЧАЛНИКА НА<br/>
                  "ПЪТНА ПОЛИЦИЯ"&#160;-&#160;<xsl:value-of select="APP:MerchantData/MD:EntityManagementAddress/EA:DistrictName" />
                </p>
              </th>
            </tr>
            <tr>
              <th colspan ="2">
                <h3>ЗАЯВЛЕНИЕ</h3>
              </th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td colspan ="2">
                От&#160;<xsl:call-template name="PersonNames">
                  <xsl:with-param name="Names" select = "APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names" />
                </xsl:call-template>,&#160;ЕГН/ЛНЧ/ЛН&#160;<xsl:value-of  select="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Identifier/."/>,<xsl:call-template name="IdentityDocument">
                  <xsl:with-param name="IdentityDocument" select = "APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument" />
                </xsl:call-template><xsl:if test="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1002'">, пълномощник на юридическо лице</xsl:if><xsl:if test="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1003'">, законен представител на юридическо лице</xsl:if>
                &#160;<xsl:value-of  select="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Name/."/>&#160;ЕИК/БУЛСТАТ:&#160;
                <xsl:value-of  select="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Identifier/."/>
                <xsl:if test="APP:MerchantData/MD:CompanyCase/MD:Number">
                  <br/>Регистриран по дело № &#160;<xsl:value-of  select="APP:MerchantData/MD:CompanyCase/MD:Number"/>
                </xsl:if>
                <xsl:if test="APP:MerchantData/MD:CompanyCase/MD:CourtName">
                  <br/>на Окръжен съд &#160;<xsl:value-of  select="APP:MerchantData/MD:CompanyCase/MD:CourtName"/>
                </xsl:if>
                <br/>Седалище и адрес на управление:&#160;<xsl:call-template name="EntityAddress">
                  <xsl:with-param name="EntityAddress" select = "APP:MerchantData/MD:EntityManagementAddress" />
                </xsl:call-template>
                <br/>Адрес за кореспонденция:&#160;<xsl:call-template name="EntityAddress">
                  <xsl:with-param name="EntityAddress" select = "APP:MerchantData/MD:CorrespondingAddress" />
                </xsl:call-template>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                Телефон:&#160;<xsl:choose>
                  <xsl:when test="APP:MerchantData/MD:Phone">
                    <xsl:value-of  select="APP:MerchantData/MD:Phone"/>
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                <xsl:call-template name="ApplicationElectronicServiceApplicantEmail">
                  <xsl:with-param name="ElectronicServiceApplicant" select = "APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant" />
                </xsl:call-template>
              </td>
            </tr>
            <tr>
              <td colspan ="2" align="center">
                Господин началник,
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                Моля, да бъдат предоставени за ползване&#160;<xsl:value-of  select="APP:ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsData/AFIOTRPTMD:TemporaryPlatesCount"/>&#160;комплекта временни табели с регистрационни номера за придвижване на автомобили, собственост на търговеца.
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                Търговецът е вносител/производител на нови автомобили от следните марки:&#160;<xsl:value-of  select="APP:ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsData/AFIOTRPTMD:OperationalNewVehicleMakes"/>.
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                Търговецът извършва търговска дейност с автомобили, втора употреба, от следните марки:&#160;<xsl:value-of  select="APP:ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsData/AFIOTRPTMD:OperationalSecondHandVehicleMakes"/>.
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                Дневникът за завеждане на табелите ще се съхранява на адрес:&#160;<xsl:call-template name="EntityAddress">
                  <xsl:with-param name="EntityAddress" select = "APP:ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsData/AFIOTRPTMD:VehicleDealershipAddress" />
                </xsl:call-template>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                За водене на дневника ще бъде/ат упълномощен/и:
              </td>
            </tr>
            <xsl:choose>
              <xsl:when test = "APP:ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsData/AFIOTRPTMD:AuthorizedPersons">
                <tr>
                  <td colspan="2">
                    <ol>
                      <xsl:for-each select="APP:ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsData/AFIOTRPTMD:AuthorizedPersons">
                        <li>
                          <xsl:value-of select="AFIOTRPTMD:FullName"/>,&#160;ЕГН/ЛНЧ/ЛН:&#160;<xsl:if test="AFIOTRPTMD:Identifier/PI:EGN">
                            <xsl:value-of  select="AFIOTRPTMD:Identifier/PI:EGN"/>
                          </xsl:if><xsl:if test="AFIOTRPTMD:Identifier/PI:LNCh">
                            <xsl:value-of  select="AFIOTRPTMD:Identifier/PI:LNCh"/>
                          </xsl:if>
                        </li>
                      </xsl:for-each>
                    </ol>
                  </td>
                </tr>
              </xsl:when>
            </xsl:choose>
            <tr>
              <td colspan ="2">
                Справка за временните табели с регистрационен номер ще може да се прави на телефон:&#160;<xsl:value-of select="APP:ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsData/AFIOTRPTMD:Phone"/>
              </td>
            </tr>
            <xsl:call-template name="AgreementToReceiveERefusal">
              <xsl:with-param name="AgreementToReceiveERefusal" select = "APP:ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsData/AFIOTRPTMD:AgreementToReceiveERefusal" />
            </xsl:call-template>
            <xsl:call-template name="Declarations">
              <xsl:with-param name="Declarations" select = "APP:Declarations" />
              <xsl:with-param name="Declaration" select = "APP:Declarations/APP:Declaration" />
            </xsl:call-template>
            <xsl:call-template name="AttachedDocuments">
              <xsl:with-param name="AttachedDocuments" select = "APP:AttachedDocuments" />
              <xsl:with-param name="AttachedDocument" select = "APP:AttachedDocuments/APP:AttachedDocument" />
            </xsl:call-template>
            <xsl:call-template name="ApplicationElectronicAdministrativeServiceFooterLite">
              <xsl:with-param name="ElectronicAdministrativeServiceFooter" select = "APP:ElectronicAdministrativeServiceFooter" />
            </xsl:call-template>
          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>