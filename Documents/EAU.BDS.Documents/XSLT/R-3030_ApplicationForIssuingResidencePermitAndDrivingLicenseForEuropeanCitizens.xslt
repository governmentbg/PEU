<xsl:stylesheet version="1.0" xmlns:DLFEUC="http://ereg.egov.bg/segment/R-3030"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:DLFEUCD="http://ereg.egov.bg/segment/R-3036"
                xmlns:PI="http://ereg.egov.bg/segment/R-3005"
                xmlns:ID="http://ereg.egov.bg/segment/R-3004"
                xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
                xmlns:NMLN="http://ereg.egov.bg/segment/R-3003"
                xmlns:ADR="http://ereg.egov.bg/segment/0009-000094"
                xmlns:GENDER="http://ereg.egov.bg/segment/0009-000156"
                xmlns:FIBD="http://ereg.egov.bg/segment/R-3022"
                xmlns:FCD="http://ereg.egov.bg/segment/0009-000109"
                xmlns:FCN ="http://ereg.egov.bg/segment/0009-000007"
				        xmlns:TD = "http://ereg.egov.bg/segment/R-3023"
                xmlns:CRBD="http://ereg.egov.bg/segment/0009-000110"
				        xmlns:IPAS="http://ereg.egov.bg/segment/R-3043"
				        xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
				        xmlns:EASF="http://ereg.egov.bg/segment/0009-000153"
				        xmlns:PDC="http://ereg.egov.bg/segment/R-3037"
				        xmlns:DURI="http://ereg.egov.bg/segment/0009-000001"
				        xmlns:CN="http://ereg.egov.bg/segment/R-3020"
				        xmlns:CH="http://ereg.egov.bg/segment/R-3049"
				        xmlns:C="http://ereg.egov.bg/segment/0009-000133"
                xmlns:xslExtension="urn:XSLExtension"
				        xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
                xmlns:DECL="http://ereg.egov.bg/segment//R-3136"
                xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

  <xsl:include href="./BDSBaseTemplates.xslt"/>
  <xsl:param name="SignatureXML"></xsl:param>

  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizens">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>
        <table border="0" cellspacing="0" width="100%" style="font-family: sans-serif; font-size: 14px;horiz-align: center ; ">
          <thead width="100%">
            <tr>
              <td style="border: none;" rowspan="5" align="center">
                <xsl:choose>
                  <xsl:when test="DLFEUC:IdentificationPhotoAndSignature/IPAS:IdentificationPhoto">
                    <div width="200" height="300">
                      <img  width="150" height="200">
                        <xsl:attribute name="src" >
                          <xsl:value-of select="concat('data:image/gif;base64,',DLFEUC:IdentificationPhotoAndSignature/IPAS:IdentificationPhoto)"/>
                        </xsl:attribute>
                      </img>
                    </div>
                  </xsl:when>
                  <xsl:otherwise>
                    <div width="200" height="300">
                      &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;<br/>&#160;<br/>&#160;<br/>&#160;<br/>&#160;<br/>&#160;<br/>&#160;<br/>&#160;<br/>&#160;<br/>&#160;<br/>&#160;<br/>&#160;<br/>&#160;
                    </div>
                  </xsl:otherwise>
                </xsl:choose>
                <br/>
                <br />
                <xsl:choose>
                  <xsl:when test="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:IdentificationPhotoAndSignature/IPAS:IdentificationSignature/.">
                    <xsl:value-of select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:IdentificationPhotoAndSignature/IPAS:IdentificationSignature/." />
                  </xsl:when>
                  <xsl:otherwise>
                    <div width="160" height="60">
                      &#160;<br/>&#160;<br/>&#160;<br/>&#160;
                    </div>
                  </xsl:otherwise>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <td>
                До: <xsl:value-of select="DLFEUC:IssuingPoliceDepartment/PDC:PoliceDepartmentName/." />
              </td>
            </tr>
            <tr>
              <td style="padding: 0px;" align="center">
                <h3>
                  <b>ЗАЯВЛЕНИЕ</b><br/>за издаване на удостоверение за пребиваване и<br/>свидетелство за управление на МПС на граждани на ЕС
                </h3>

              </td>
            </tr>
            <tr>
              <td style="padding: 0px;">
                &#160;
              </td>
            </tr>

          </thead>
          <tbody width="100%">
            <tr>
              <td colspan="2">
                <table border="1" cellspacing="0" style="width:100%; height: 100%;border: solid 1px black;border-collapse: collapse;font-size: 14px; ">
                  <tr>
                    <td colspan="2">
                      <table border="0" cellspacing="0" style="padding-bottom: 0px;font-size: 14px;">
                        <tr>
                          <td colspan="2">
                            <b>Вид на услугата:</b>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="2">
                            <xsl:choose>
                              <xsl:when test="DLFEUC:ServiceTermType='0006-000083'">
                                обикновена <input type="checkbox" checked="true" disabled="" readonly="" /> бърза <input type="checkbox" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:when test="DLFEUC:ServiceTermType='0006-000084'">
                                обикновена <input type="checkbox" disabled="" readonly="" /> бърза <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                обикновена <input type="checkbox" disabled="" readonly="" /> бърза <input type="checkbox" disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="2">
                            <b>Вид на искания документ :</b>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <xsl:choose>
                              <xsl:when test="DLFEUC:IdentificationDocuments/DLFEUC:IdentificationDocumentType='0006-000099'">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                            Удостоверение за пребиваване на гражданин на ЕС
                          </td>
                          <td>
                            <xsl:choose>
                              <xsl:when test="DLFEUC:IdentificationDocuments/DLFEUC:IdentificationDocumentType='0006-000093'">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                            Свидетелство за управление на моторно превозно средство
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      <b>Пребиваване в РБ:</b>
                      <xsl:value-of select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:StayInBulgaria/. " />
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      <b>Личен номер:</b>
                      <xsl:value-of select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:LNCh " />
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      <table width="100%" style="font-size: 14px;">
                        <tr>
                          <td style="width: 95%; text-align: left;">
                            Имена (по национален документ)
                          </td>
                        </tr>
                        <tr>
                          <td style=" text-align: center; width: 90%;">
                            <xsl:value-of  select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:FirstLatin/."/>
                            &#160;
                            <xsl:value-of  select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:OtherLatin/."/>
                            &#160;
                            <xsl:value-of  select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:LastLatin/."/>
                          </td>
                        </tr>
                        <tr>
                          <td style="width: 95%; text-align: left;">
                            Имена(на кирилица)
                          </td>
                        </tr>
                        <tr>
                          <td style="text-align: center; width: 90%;" >
                            <xsl:value-of  select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:FirstCyrillic/."/>
                            &#160;
                            <xsl:value-of  select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:OtherCyrillic/."/>
                            &#160;
                            <xsl:value-of  select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:LastCyrillic/."/>

                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td >
                      Дата на раждане:
                      <xsl:choose>
                        <xsl:when test="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:BirthDate">
                          <xsl:choose>
                            <xsl:when test="string-length(DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:BirthDate)>7">
                              <xsl:value-of  select="substring(DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:BirthDate,1,2)"/>.<xsl:value-of  select="substring(DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:BirthDate,3,2)"/>.<xsl:value-of  select="substring(DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:BirthDate,5)"/> г.
                            </xsl:when>
                            <xsl:otherwise>
                              00.<xsl:value-of  select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:BirthDate"/> г.
                            </xsl:otherwise>
                          </xsl:choose>

                        </xsl:when>
                      </xsl:choose>
                      &#160;&#160;&#160;
                      Място на раждане:&#160;
                      <xsl:choose>
                        <xsl:when test="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:PlaceOfBirth/.">
                          <xsl:value-of select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:PlaceOfBirth/." />
                        </xsl:when>
                        <xsl:otherwise>
                          <xsl:value-of select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:PlaceOfBirthAbroad/." />
                        </xsl:otherwise>
                      </xsl:choose>
                    </td>

                  </tr>

                  <tr>
                    <td>
                      Пол <xsl:choose>
                        <xsl:when test="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:GenderName ='Male' or DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:GenderName ='MALE'">
                          <b>М</b>
                        </xsl:when>
                        <xsl:otherwise>
                          <b>Ж</b>
                        </xsl:otherwise>
                      </xsl:choose>
                      &#160;&#160;&#160;
                      Гражданство:&#160;
                      <xsl:for-each select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:Citizenships/FCD:Citizenship">
                        <xsl:value-of  select="C:CountryName"/>
                        <br/>
                      </xsl:for-each>

                    </td>
                  </tr>
                  <tr>
                    <td >
                      <table width="100%" style="font-size: 12px;">
                        <tr>
                          <td align="left" style="font-size: 14px;" colspan="5">
                            <b>Документ за задгранично пътуване</b>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            Серия и номер: <xsl:value-of select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:TravelDocument/TD:IdentityNumber"/>
                          </td>
                          <td>
                            Дата на издаване: <xsl:value-of select="ms:format-date(DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:TravelDocument/TD:IdentitityIssueDate, 'dd.MM.yyyy') "/> г.
                          </td>
                          <td>
                            Валиден до: <xsl:value-of select="ms:format-date(DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:TravelDocument/TD:IdentitityExpireDate, 'dd.MM.yyyy') "/> г.
                          </td>
                          <td>
                            Място на издаване: <xsl:value-of select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:TravelDocument/TD:IdentityIssuer"/>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td >
                      <table width="100%" style="font-size:14px;">
                        <tr>
                          <td align="left" colspan="3">
                            <b>Адрес:</b>
                          </td>
                        </tr>
                        <tr>
                          <td >
                            Oбласт <xsl:value-of  select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:Address/ADR:DistrictGRAOName/."/>
                          </td>
                          <td colspan="2">
                            Oбщина <xsl:value-of  select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:Address/ADR:MunicipalityGRAOName/."/>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="3">
                            <xsl:value-of  select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:Address/ADR:SettlementGRAOName/."/>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <xsl:value-of  select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:Address/ADR:StreetText/."/>
                          </td>
                          <td>
                            № <xsl:value-of  select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:Address/ADR:BuildingNumber/."/>
                          </td>
                          <td>
                            Вх.<xsl:value-of  select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:Address/ADR:Entrance/."/>,
                            Ет.<xsl:value-of  select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:Address/ADR:Floor/."/>,
                            Ап.<xsl:value-of  select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:Address/ADR:Apartment/."/>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <b>Други гражданства:</b>
                      <br/>
                      <xsl:value-of select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:OtherCitizenship/CH:CountryName"/>
                    </td>
                  </tr>
                  <tr>
                    <td >
                      <table width="100%" style="font-size:14px;">
                        <tr>
                          <td>
                            Семейно положение:
                          </td>

                          <td>
                            <xsl:choose>
                              <xsl:when test="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:MaritalStatus='357'">
                                <u>
                                  <b>женен/омъжена</b>
                                </u>
                              </xsl:when>
                              <xsl:otherwise>
                                женен/омъжена
                              </xsl:otherwise>
                            </xsl:choose>
                          </td>

                          <td>
                            <xsl:choose>
                              <xsl:when test="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:MaritalStatus='358'">
                                <u>
                                  <b>разведен/а</b>
                                </u>
                              </xsl:when>
                              <xsl:otherwise>
                                разведен/а
                              </xsl:otherwise>
                            </xsl:choose>

                          </td>
                          <td>
                            <xsl:choose>
                              <xsl:when test="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:MaritalStatus='355'">
                                <u>
                                  <b>вдовец/вдовица</b>
                                </u>
                              </xsl:when>
                              <xsl:otherwise>
                                вдовец/вдовица
                              </xsl:otherwise>
                            </xsl:choose>
                          </td>
                          <td>
                            <xsl:choose>
                              <xsl:when test="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:MaritalStatus='356'">
                                <u>
                                  <b>неженен/неомъжена</b>
                                </u>
                              </xsl:when>
                              <xsl:otherwise>
                                неженен/неомъжена
                              </xsl:otherwise>
                            </xsl:choose>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="3">
                            Образование
                          </td>
                          <td colspan="2">
                            Тел. за връзка:&#160;<xsl:value-of select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUCD:ForeignIdentityBasicData/FIBD:Phone"/>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>

                  <tr>
                    <td>
                      <table width="100%"  style="font-size:14px;">
                        <tr>
                          <td>
                            Дата:&#160;<xsl:value-of select="ms:format-date(DLFEUC:ElectronicAdministrativeServiceHeader/EASH:DocumentURI/DURI:ReceiptOrSigningDate , 'dd.MM.yyyy') "/> г.
                          </td>
                        </tr>
                        <tr>
                          <td>
                            Приел и регистрирал заявлението:
                          </td>
                        </tr>

                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td  >
                      Служебна информация: <xsl:value-of  select="DLFEUC:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData/DLFEUC:ServiceInformation"/>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <xsl:choose>
              <xsl:when test = "DLFEUC:Declarations">
                <xsl:for-each select="DLFEUC:Declarations/DLFEUC:Declaration">
                  <xsl:choose>
                    <xsl:when test="DECL:IsDeclarationFilled = 'true'">
                      <tr>
                        <td colspan="2">
                          <xsl:value-of  select="DECL:DeclarationName" disable-output-escaping="yes"/>
                        </td>
                      </tr>
                      <xsl:choose>
                        <xsl:when test="DECL:FurtherDescriptionFromDeclarer">
                          <tr>
                            <td colspan="2">
                              Декларирам (допълнително описание на обстоятелствата по декларацията):<xsl:value-of  select="DECL:FurtherDescriptionFromDeclarer"/>
                            </td>
                          </tr>
                          <tr>
                            <td colspan="2">
                              <xsl:value-of  select="DECL:FurtherDescriptionFromDeclarer"/>
                            </td>
                          </tr>
                        </xsl:when>
                      </xsl:choose>
                    </xsl:when>
                  </xsl:choose>

                </xsl:for-each>
              </xsl:when>
            </xsl:choose>
            <tr>
              <td width="50%">
                Дата:&#160;<xsl:value-of  select="ms:format-date(DLFEUC:ElectronicAdministrativeServiceFooter/EASF:ApplicationSigningTime , 'dd.MM.yyyy')"/>г.
              </td>
              <td width="50%">
                <xsl:call-template name="DocumentSignatures">
                  <xsl:with-param name="Signatures" select = "$SignatureXML/DocumentSignatures" />
                </xsl:call-template>
              </td>
            </tr>
          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>

</xsl:stylesheet>