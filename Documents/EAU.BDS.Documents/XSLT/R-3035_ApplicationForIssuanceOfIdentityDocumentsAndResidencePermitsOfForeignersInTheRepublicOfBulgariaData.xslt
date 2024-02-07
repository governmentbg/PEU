<xsl:stylesheet version="1.0" xmlns:AFIIDARPFRB="http://ereg.egov.bg/segment/R-3021"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:AFIIDARPFRBD="http://ereg.egov.bg/segment/R-3035"
				        xmlns:IBD="http://ereg.egov.bg/segment/R-3017"
                xmlns:PI="http://ereg.egov.bg/segment/R-3005"
                xmlns:ID="http://ereg.egov.bg/segment/R-3004"
				        xmlns:IDENT ="http://ereg.egov.bg/segment/0009-000006"
				        xmlns:WFBC="http://ereg.egov.bg/segment/R-3024"
				        xmlns:ETROB="http://ereg.egov.bg/segment/R-3025"
				        xmlns:VD="http://ereg.egov.bg/segment/R-3026"
				        xmlns:PTGD="http://ereg.egov.bg/segment/R-3019"
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
				        xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
				        xmlns:CLFP="http://ereg.egov.bg/segment/R-3028"
                xmlns:DECL="http://ereg.egov.bg/segment//R-3136"
                xmlns:C="http://ereg.egov.bg/segment/0009-000133"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

  <xsl:include href="./BDSBaseTemplates.xslt"/>
  <xsl:param name="SignatureXML"></xsl:param>
  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgaria">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>
        <table border="0" cellspacing="0" width="100%" style="font-family: sans-serif; font-size: 13px;horiz-align: center ; ">
          <thead width="100%">
            <tr>
              <td style="border: none;" rowspan="5" align="center">
                <xsl:choose>
                  <xsl:when test="AFIIDARPFRB:IdentificationPhotoAndSignature/IPAS:IdentificationPhoto">
                    <div width="200" height="300">
                      <img  width="150" height="200">
                        <xsl:attribute name="src" >
                          <xsl:value-of select="concat('data:image/gif;base64,',AFIIDARPFRB:IdentificationPhotoAndSignature/IPAS:IdentificationPhoto)"/>
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
                  <xsl:when test="AFIIDARPFRB:IdentificationPhotoAndSignature/IPAS:IdentificationSignature/.">
                    <xsl:value-of select="AFIIDARPFRB:IdentificationPhotoAndSignature/IPAS:IdentificationSignature/." />
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
                До: <xsl:value-of select="AFIIDARPFRB:IssuingPoliceDepartment/PDC:PoliceDepartmentName/." /><br/>
                <!--Рег.номер: <xsl:value-of select="AFIIDARPFRB:ElectronicAdministrativeServiceHeader/EASH:DocumentURI/DURI:RegisterIndex/." />-<xsl:value-of select="AFIIDARPFRB:ElectronicAdministrativeServiceHeader/EASH:DocumentURI/DURI:SequenceNumber/." />/<xsl:value-of select="ms:format-date(AFIIDARPFRB:ElectronicAdministrativeServiceHeader/EASH:DocumentURI/DURI:ReceiptOrSigningDate/. , 'dd.MM.yyyy') " />-->
              </td>
            </tr>
            <tr>
              <td style="padding: 0px;" align="center">
                <h4>
                  <b>ЗАЯВЛЕНИЕ</b><br/>за издаване на документи за самоличност и пребиваване<br/>на чужденци в Република България
                </h4>

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
                <table border="1" cellspacing="0" style="width:100%; height: 100%;border: solid 1px black;border-collapse: collapse;font-size: 13px; ">
                  <tr>
                    <td colspan="2">
                      <table border="0" cellspacing="0" style="padding-bottom: 0px;font-size: 13px;">
                        <tr>
                          <td colspan="2">
                            <b>Вид на услугата:</b>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="2">
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ServiceTermType='0006-000083'">
                                обикновена <input type="checkbox" checked="true" disabled="" readonly="" /> бърза <input type="checkbox" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:when test="AFIIDARPFRB:ServiceTermType='0006-000084'">
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
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PreviousIdentityDocument/IBD:IdentityDocumentType='0006-000099'">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                            Разрешение за пребиваване
                          </td>
                          <td>
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PreviousIdentityDocument/IBD:IdentityDocumentType='0006-000099'">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                            Карта на бежанец
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PreviousIdentityDocument/IBD:IdentityDocumentType='0006-000099'">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                            Удостоверение за пътуване на лице без гражданство
                          </td>
                          <td>
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PreviousIdentityDocument/IBD:IdentityDocumentType='0006-000099'">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                            Карта на чужденец получил убежище
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PreviousIdentityDocument/IBD:IdentityDocumentType='0006-000099'">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                            Временно удостоверение за напускане на Република България
                          </td>
                          <td>
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PreviousIdentityDocument/IBD:IdentityDocumentType='0006-000099'">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                            Карта на чужденец с хуманитарен статут
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PreviousIdentityDocument/IBD:IdentityDocumentType='0006-000099'">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                            Удостоверение за завръщане в Република България на чужденец
                          </td>
                          <td>
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PreviousIdentityDocument/IBD:IdentityDocumentType='0006-000099'">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                            Удостоверение за пътуване зад граница на бежанец
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PreviousIdentityDocument/IBD:IdentityDocumentType='0006-000099'">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                            Временна карта за чужденец
                          </td>
                          <td>
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PreviousIdentityDocument/IBD:IdentityDocumentType='0006-000099'">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                            Удостоверение за пътуване зад граница на чужденец, получил убежище
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PreviousIdentityDocument/IBD:IdentityDocumentType='0006-000093'">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                            Свидетелство за управление на моторно превозно средство
                          </td>
                          <td>
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PreviousIdentityDocument/IBD:IdentityDocumentType='0006-000099'">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                            Удостоверение за пътуване зад граница на чужденец с хуманитарен статут
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PreviousIdentityDocument/IBD:IdentityDocumentType='0006-000099'">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                            Моряшки паспорт
                          </td>
                          <td>
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PreviousIdentityDocument/IBD:IdentityDocumentType='0006-000099'">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                            Карта на чужденец, акредитиран като служител в дипломатическо или консулско представителство или в международна организация със седалище на територията на Република България
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      <table width="100%" style="font-size: 13px;">
                        <tr>
                          <td align="left" colspan="5">
                            <b>Предишен документ</b>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            Номер: <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PreviousIdentityDocument/IBD:IdentityNumber"/>
                          </td>
                          <td>
                            Дата на издаване: <xsl:value-of select="ms:format-date(AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PreviousIdentityDocument/IBD:IdentitityIssueDate/. , 'dd.MM.yyyy') "/> г.
                          </td>
                          <td>
                            Валиден до: <xsl:value-of select="ms:format-date(AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PreviousIdentityDocument/IBD:IdentitityExpireDate/. , 'dd.MM.yyyy') "/> г.
                          </td>
                          <td>
                            Място на издаване: <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PreviousIdentityDocument/IBD:IdentityIssuer"/>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      <table width="100%" style="font-size: 13px;">
                        <tr>
                          <td align="left" colspan="5">
                            <b>Други документи за самоличност</b>
                          </td>
                        </tr>
                        <tr>
                          <xsl:choose>
                            <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:OtherIdentityDocument/IBD:IdentityNumber">
                              <td>
                                Номер: <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:OtherIdentityDocument/IBD:IdentityNumber"/>
                              </td>
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:OtherIdentityDocument/IBD:IdentitityIssueDate">
                              <td>
                                Дата на издаване: <xsl:value-of select="ms:format-date(AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:OtherIdentityDocument/IBD:IdentitityIssueDate, 'dd.MM.yyyy') "/> г.
                              </td>
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:OtherIdentityDocument/IBD:IdentitityExpireDate">
                              <td>
                                Валиден до: <xsl:value-of select="ms:format-date(AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:OtherIdentityDocument/IBD:IdentitityExpireDate, 'dd.MM.yyyy') "/> г.
                              </td>
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:OtherIdentityDocument/IBD:IdentityIssuer">
                              <td>
                                Място на издаване: <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:OtherIdentityDocument/IBD:IdentityIssuer"/>
                              </td>
                            </xsl:when>
                          </xsl:choose>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td >
                      <xsl:choose>
                        <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:LNCh">
                          <b>ЛНЧ:</b>
                          <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:LNCh" />
                        </xsl:when>

                      </xsl:choose>
                    </td>
                    <td>
                      <xsl:choose>
                        <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:EGN">
                          <b>ЕГН:</b>
                          <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:EGN" />
                        </xsl:when>
                      </xsl:choose>

                    </td>
                  </tr>

                  <tr>
                    <td colspan="2">
                      <table width="100%" style="font-size: 13px;">
                        <tr>
                          <td style="width: 95%; text-align: left;">
                            Имена (по паспорт/лична карта)
                          </td>
                        </tr>
                        <tr>
                          <td style=" text-align: center; width: 90%;">
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:FirstLatin/."/>
                            &#160;
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:OtherLatin/."/>
                            &#160;
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:LastLatin/."/>
                          </td>
                        </tr>
                        <tr>
                          <td style="width: 95%; text-align: left;">
                            Имена(на кирилица)
                          </td>
                        </tr>
                        <tr>
                          <td style="text-align: center; width: 90%;" >
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:FirstCyrillic/."/>
                            &#160;
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:OtherCyrillic/."/>
                            &#160;
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:LastCyrillic/."/>

                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      Дата на раждане:
                      <xsl:choose>
                        <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:BirthDate">
                          <xsl:choose>
                            <xsl:when test="string-length(AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:BirthDate)>7">
                              <xsl:value-of  select="substring(AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:BirthDate, 1, 2) "/>.<xsl:value-of  select="substring(AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:BirthDate, 3, 2) "/>.<xsl:value-of  select="substring(AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:BirthDate, 5) "/> г.
                            </xsl:when>
                            <xsl:otherwise>
                              00.<xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:BirthDate "/> г.
                            </xsl:otherwise>
                          </xsl:choose>

                        </xsl:when>
                      </xsl:choose>
                      &#160;&#160;&#160;
                      Място на раждане:&#160;
                      <xsl:choose>
                        <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:PlaceOfBirth/.">
                          <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:PlaceOfBirth/." />
                        </xsl:when>
                        <xsl:otherwise>
                          <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:PlaceOfBirthAbroad/." />
                        </xsl:otherwise>
                      </xsl:choose>
                    </td>

                  </tr>

                  <tr>
                    <td colspan="2">
                      Пол <xsl:choose>
                        <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:GenderName ='Male' or AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:GenderName ='MALE'">
                          <b>М</b>
                        </xsl:when>
                        <xsl:otherwise>
                          <b>Ж</b>
                        </xsl:otherwise>
                      </xsl:choose>
                      &#160;&#160;&#160;
                      Гражданство:&#160; <xsl:for-each select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:ForeignCitizenData/FCD:Citizenships/FCD:Citizenship">
                        <xsl:value-of  select="C:CountryName"/>
                        <br/>
                      </xsl:for-each>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      <b>Други гражданства:</b>
                      <br/>
                      <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:OtherCitizenship/CH:CountryName"/>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </tbody>
        </table>
        <p style="page-break-before: always"></p>
        <table border="0" cellspacing="0" width="100%" style="font-family: sans-serif; font-size: 13px;horiz-align: center ; ">
          <tbody width="100%">
            <tr>
              <td colspan="2">
                <table border="1" cellspacing="0" style="width:100%; height: 100%;border: solid 1px black;border-collapse: collapse;font-size: 13px; ">
                  <tr>
                    <td >
                      <table width="100%" style="font-size: 12px;">
                        <tr>
                          <td align="left" colspan="4">
                            <b>Документ за задгранично пътуване</b>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            Серия и номер: <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:TravelDocument/TD:IdentityDocumentSeries"/><xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:TravelDocument/TD:IdentityNumber"/>
                          </td>
                          <td>
                            Дата на издаване: <xsl:value-of select="ms:format-date(AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:TravelDocument/TD:IdentitityIssueDate, 'dd.MM.yyyy') "/> г.
                          </td>
                          <td>
                            Валиден до: <xsl:value-of select="ms:format-date(AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:TravelDocument/TD:IdentitityExpireDate, 'dd.MM.yyyy') "/> г.
                          </td>
                          <td>
                            Място на издаване: <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:TravelDocument/TD:IdentityIssuer/CH:CountryName"/>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            &#160;
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      <b>Пребиваване в РБ:</b>
                      <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:StayInBulgaria/. " />
                    </td>
                  </tr>
                  <tr>
                    <td >
                      <table width="100%" style="font-size:13px;">
                        <tr>
                          <td align="left" colspan="3">
                            <b>Постоянен адрес:</b>
                          </td>
                        </tr>
                        <tr>
                          <td >
                            Oбласт <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PermanentAddress/ADR:DistrictGRAOName/."/>
                          </td>
                          <td colspan="2">
                            Oбщина <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PermanentAddress/ADR:MunicipalityGRAOName/."/>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="3">
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PermanentAddress/ADR:SettlementGRAOName/."/>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PermanentAddress/ADR:StreetText/."/>
                          </td>
                          <td>
                            № <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PermanentAddress/ADR:BuildingNumber/."/>
                          </td>
                          <td>
                            Вх.<xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PermanentAddress/ADR:Entrance/."/>,
                            Ет.<xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PermanentAddress/ADR:Floor/."/>,
                            Ап.<xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PermanentAddress/ADR:Apartment/."/>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            &#160;
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td >
                      <table width="100%" style="font-size:13px;">
                        <tr>
                          <td align="left" colspan="3">
                            <b>Настоящ адрес:</b>
                          </td>
                        </tr>
                        <tr>
                          <td >
                            Oбласт <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PresentAddress/ADR:DistrictGRAOName/."/>
                          </td>
                          <td colspan="2">
                            Oбщина <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PresentAddress/ADR:MunicipalityGRAOName/."/>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="3">
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PresentAddress/ADR:SettlementGRAOName/."/>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PresentAddress/ADR:StreetText/."/>
                          </td>
                          <td>
                            № <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PresentAddress/ADR:BuildingNumber/."/>
                          </td>
                          <td>
                            Вх.<xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PresentAddress/ADR:Entrance/."/>,
                            Ет.<xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PresentAddress/ADR:Floor/."/>,
                            Ап.<xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:PresentAddress/ADR:Apartment/."/>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            &#160;
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      <b>Променени(предишни) имена</b>
                      <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ChangedNames/CN:Names/NM:First " />
                      <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ChangedNames/CN:Names/NM:Middle " />
                      <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ChangedNames/CN:Names/NM:Last " />
                    </td>
                  </tr>
                  <tr>
                    <td >
                      <table width="100%" style="font-size:13px;">
                        <tr>
                          <td>
                            <b>Било ли е лицето български гражданин?:</b>
                          </td>
                          <td>
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:WasForeignerBulgarianCitizen/WFBC:WasPersonBulgarianCitizen">
                                Да<input type="checkbox"  disabled="" readonly="" style="font-weight: bold;" />
                                Не<input type="checkbox"  disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                Да<input type="checkbox"  disabled="" readonly="" />
                                Не<input type="checkbox"  disabled="" readonly="" style="font-weight: bold;" />
                              </xsl:otherwise>
                            </xsl:choose>
                          </td>
                          <td>
                            От дата
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:WasForeignerBulgarianCitizen/WFBC:FromDate">
                                <xsl:value-of select="ms:format-date(AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:WasForeignerBulgarianCitizen/WFBC:FromDate, 'dd.MM.yyyy') "/> г.
                              </xsl:when>
                            </xsl:choose>

                          </td>
                          <td>
                            До дата
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:WasForeignerBulgarianCitizen/WFBC:ToDate">
                                <xsl:value-of select="ms:format-date(AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:WasForeignerBulgarianCitizen/WFBC:ToDate, 'dd.MM.yyyy') "/> г.
                              </xsl:when>
                            </xsl:choose>
                          </td>

                        </tr>
                        <tr>
                          <td >
                            Под какви имена:
                          </td>
                          <td colspan="3">
                            <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:WasForeignerBulgarianCitizen/WFBC:ForeignCitizenNames/FCN:FirstLatin/."/>
                            <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:WasForeignerBulgarianCitizen/WFBC:ForeignCitizenNames/FCN:OtherLatin/."/>
                            <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:WasForeignerBulgarianCitizen/WFBC:ForeignCitizenNames/FCN:LastLatin/."/>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            &#160;
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <table width="100%" style="font-size:13px;">
                        <tr>
                          <td>
                            Цвят на очите:
                          </td>
                          <td>
                            черни
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:EyesColor='3292'">
                                <input type="checkbox" checked="true" disabled="" readonly="" style="font-weight: bold;" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                          </td>
                          <td>
                            кафяви
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:EyesColor='1288'">
                                <input type="checkbox" checked="true" disabled="" readonly=""  style="font-weight: bold;"/>
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                          </td>
                          <td>
                            сини
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:EyesColor='2698'">
                                <input type="checkbox" checked="true" disabled="" readonly="" style="font-weight: bold;" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                          </td>
                          <td>
                            сиви
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:EyesColor='2704'">
                                <input type="checkbox" checked="true" disabled="" readonly="" style="font-weight: bold;" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                          </td>
                          <td>
                            зелени
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:EyesColor='3227'">
                                <input type="checkbox" checked="true" disabled="" readonly="" style="font-weight: bold;" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                          </td>
                          <td>
                            пъстри
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:EyesColor='2472'">
                                <input type="checkbox" checked="true" disabled="" readonly="" style="font-weight: bold;" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                          </td>
							<td>
								червени
								<xsl:choose>
									<xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:EyesColor='21773'">
										<input type="checkbox" checked="true" disabled="" readonly="" style="font-weight: bold;" />
									</xsl:when>
									<xsl:otherwise>
										<input type="checkbox"  disabled="" readonly="" />
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td>
								различни
								<xsl:choose>
									<xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:EyesColor='22073'">
										<input type="checkbox" checked="true" disabled="" readonly="" style="font-weight: bold;" />
									</xsl:when>
									<xsl:otherwise>
										<input type="checkbox"  disabled="" readonly="" />
									</xsl:otherwise>
								</xsl:choose>
							</td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td>

                      <table width="100%" style="font-size:13px;">
                        <tr>
                          <td>
                            <b>Ръст(в см.):&#160;</b>
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:Height"/>
                          </td>
                          <td>
                            <b>Телефон за връзка:</b>&#160;
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:Phone"/>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <b>Адрес в чужбина:</b>&#160;<xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:AbroadAddress"/>
                    </td>
                  </tr>
                  <tr>
                    <td >
                      <table width="100%" style="font-size:13px;">
                        <tr>
                          <td colspan="5">
                            <b>Влизане в република България:</b>
                            Дата: <xsl:value-of  select="ms:format-date(AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:EntranceInTheRepublicOfBulgaria/ETROB:EntranceInCountyrDate, 'dd.MM.yyyy') "/> г.
                            ГКПП: <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:EntranceInTheRepublicOfBulgaria/ETROB:BorderCheckpoint"/>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            Без виза
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:EntranceInTheRepublicOfBulgaria/ETROB:VisaDocument/VD:VisaTypes = 1">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                          </td>
                          <td>
                            С виза/летищен трансфер
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:EntranceInTheRepublicOfBulgaria/ETROB:VisaDocument/VD:VisaTypes = 2">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                          </td>
                          <td>
                            Краткосрочна
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:EntranceInTheRepublicOfBulgaria/ETROB:VisaDocument/VD:VisaTypes = 3">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                          </td>
                          <td>
                            Транзитна
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:EntranceInTheRepublicOfBulgaria/ETROB:VisaDocument/VD:VisaTypes = 4">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                          </td>
                          <td>
                            Дългосрочна
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:EntranceInTheRepublicOfBulgaria/ETROB:VisaDocument/VD:VisaTypes = 5">
                                <input type="checkbox" checked="true" disabled="" readonly="" />
                              </xsl:when>
                              <xsl:otherwise>
                                <input type="checkbox"  disabled="" readonly="" />
                              </xsl:otherwise>
                            </xsl:choose>
                          </td>
                        </tr>
                        <tr>
                          <td colspan ="5">
                            <b>Виза:</b>Серия:<xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:EntranceInTheRepublicOfBulgaria/ETROB:VisaDocument/VD:IdentityDocumentSeries "/>&#160;

                            Номер:<xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:EntranceInTheRepublicOfBulgaria/ETROB:VisaDocument/VD:IdentityNumber "/>&#160;
                          </td>
                          <td>
                            Място на издаване: <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:EntranceInTheRepublicOfBulgaria/ETROB:VisaDocument/VD:IdentityIssuer "/>&#160;
                          </td>
                        </tr>
                        <tr>
                          <td colspan ="5">
                            Разрешен срок: <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:EntranceInTheRepublicOfBulgaria/ETROB:VisaDocument/VD:IdentityDocumentPeriod "/>&#160;
                            Цел на идване в Р.България: <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:EntranceInTheRepublicOfBulgaria/ETROB:VisaDocument/VD:PurposeOfComing "/> &#160;
                          </td>
                        </tr>
                        <tr>
                          <td>
                            &#160;
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td >
                      <table width="100%" style="font-size:13px;">
                        <tr>
                          <td>
                            <b>Семейно положение:</b>
                          </td>

                          <td>
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:MaritalStatus='357'">
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
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:MaritalStatus='358'">
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
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:MaritalStatus='355'">
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
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ForeignIdentityBasicData/FIBD:MaritalStatus='356'">
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
                          <td colspan="5">
                            Съпруг/а(имена на латиница по паспорт)
                          </td>
                        </tr>
                        <tr>
                          <td colspan="5">
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:SpouseData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:FirstLatin "/>&#160;
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:SpouseData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:OtherLatin "/>&#160;
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:SpouseData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:LastLatin "/>&#160;
                          </td>
                        </tr>
                        <tr>
                          <td colspan="2">
                            Дата на раждане:
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:SpouseData/FIBD:ForeignCitizenData/FCD:BirthDate">
                                <xsl:choose>
                                  <xsl:when test="string-length(AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:SpouseData/FIBD:ForeignCitizenData/FCD:BirthDate)>7">
                                    <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:SpouseData/FIBD:ForeignCitizenData/FCD:BirthDate "/> г.
                                  </xsl:when>
                                  <xsl:otherwise>
                                    00.<xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:MotherData/FIBD:ForeignCitizenData/FCD:BirthDate "/> г.
                                  </xsl:otherwise>
                                </xsl:choose>

                              </xsl:when>
                            </xsl:choose>
                          </td>
                          <td colspan="2">
                            Гражданство:
                            <xsl:for-each select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:SpouseData/FIBD:ForeignCitizenData/FCD:Citizenships/FCD:Citizenship">
                              <xsl:value-of  select="CH:CountryName"/>
                              <br/>
                            </xsl:for-each>

                          </td>
                          <td>
                            <xsl:choose>
                              <xsl:when test ="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:SpouseData/FIBD:EGN">
                                ЕГН: <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:SpouseData/FIBD:EGN/."/>
                              </xsl:when>
                            </xsl:choose>
                            <xsl:choose>
                              <xsl:when test ="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:SpouseData/FIBD:LNCH">
                                ЛНЧ: <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:SpouseData/FIBD:LNCH/."/>
                              </xsl:when>
                            </xsl:choose>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            &#160;
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>

                  <tr>
                    <td >
                      <table width="100%" style="font-size:13px;">
                        <tr>
                          <td>
                            <b>Родители:</b>
                          </td>
                          <td>
                            Имена:
                          </td>
                          <td>
                            Дата на раждане:
                          </td>
                          <td>
                            Гражданство:
                          </td>
                          <td>
                            ЕГН/ЛНЧ:
                          </td>
                        </tr>
                        <tr>
                          <td>
                            Майка:
                          </td>
                          <td>
                            <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:MotherData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:FirstCyrillic"/>&#160;
                            <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:MotherData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:OtherCyrillic"/>&#160;
                            <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:MotherData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:LastCyrillic"/>&#160;
                          </td>
                          <td>
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:MotherData/FIBD:ForeignCitizenData/FCD:BirthDate">
                                <xsl:choose>
                                  <xsl:when test="string-length(AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:MotherData/FIBD:ForeignCitizenData/FCD:BirthDate)>7">
                                    <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:MotherData/FIBD:ForeignCitizenData/FCD:BirthDate "/> г.
                                  </xsl:when>
                                  <xsl:otherwise>
                                    00.<xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:MotherData/FIBD:ForeignCitizenData/FCD:BirthDate "/> г.
                                  </xsl:otherwise>
                                </xsl:choose>

                              </xsl:when>
                            </xsl:choose>
                          </td>
                          <td>
                            <xsl:for-each select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:MotherData/FIBD:ForeignCitizenData/FCD:Citizenships/FCD:Citizenship">
                              <xsl:value-of  select="CH:CountryName"/>
                              <br/>
                            </xsl:for-each>
                          </td>
                          <td>
                            <xsl:choose>
                              <xsl:when test ="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:MotherData/FIBD:EGN">
                                <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:MotherData/FIBD:EGN/."/>
                              </xsl:when>
                            </xsl:choose>
                            <xsl:choose>
                              <xsl:when test ="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:MotherData/FIBD:LNCH">
                                <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:MotherData/FIBD:LNCH/."/>
                              </xsl:when>
                            </xsl:choose>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            Баща:
                          </td>
                          <td>
                            <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:FatherData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:FirstCyrillic"/>&#160;
                            <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:FatherData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:OtherCyrillic"/>&#160;
                            <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:FatherData/FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:LastCyrillic"/>&#160;
                          </td>
                          <td>
                            <xsl:choose>
                              <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:FatherData/FIBD:ForeignCitizenData/FCD:BirthDate">
                                <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:FatherData/FIBD:ForeignCitizenData/FCD:BirthDate "/> г.
                              </xsl:when>
                            </xsl:choose>

                          </td>
                          <td>
                            <xsl:for-each select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:FatherData/FIBD:ForeignCitizenData/FCD:Citizenships/FCD:Citizenship">
                              <xsl:value-of  select="CH:CountryName"/>
                              <br/>
                            </xsl:for-each>
                          </td>
                          <td>
                            <xsl:choose>
                              <xsl:when test ="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:FatherData/FIBD:EGN">
                                <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:FatherData/FIBD:EGN/."/>
                              </xsl:when>
                            </xsl:choose>
                            <xsl:choose>
                              <xsl:when test ="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:FatherData/FIBD:LNCH">
                                <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:FatherData/FIBD:LNCH/."/>
                              </xsl:when>
                            </xsl:choose>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            &#160;
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td >
                      <table width="100%" style="font-size:13px;">
                        <tr>
                          <td>
                            <b>Братя/Сестри:</b>
                          </td>
                          <td>
                            Имена
                          </td>
                          <td>
                            Дата на раждане
                          </td>
                          <td>
                            Гражданство
                          </td>
                          <td>
                            ЕГН/ЛНЧ
                          </td>
                        </tr>
                        <tr>
                          <td>
                            1
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                        </tr>
                        <tr>
                          <td>
                            2
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                        </tr>
                        <tr>
                          <td>
                            3
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                        </tr>
                        <tr>
                          <td>
                            4
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </tbody>
        </table>
        <p style="page-break-before: always"></p>
        <table border="0" cellspacing="0" width="100%" style="font-family: sans-serif; font-size: 13px;horiz-align: center ; ">
          <tbody width="100%">
            <tr>
              <td colspan="2">
                <table border="1" cellspacing="0" style="width:100%; height: 100%;border: solid 1px black;border-collapse: collapse;font-size: 13px; ">
                  <tr>
                    <td >
                      <table width="100%" style="font-size:13px;">
                        <tr>
                          <td>
                            <b>Деца до 14г.</b>
                          </td>
                          <td>
                            Имена
                          </td>
                          <td>
                            Дата на раждане
                          </td>
                          <td>
                            Гражданство
                          </td>
                          <td>
                            ЕГН/ЛНЧ
                          </td>
                        </tr>
                        <xsl:for-each select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ChildrensData/AFIIDARPFRBD:ChildrenData">
                          <tr>
                            <td>
                              <xsl:value-of  select="FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:FirstCyrillic"/>&#160;
                              <xsl:value-of  select="FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:OtherCyrillic"/>&#160;
                              <xsl:value-of  select="FIBD:ForeignCitizenData/FCD:ForeignCitizenNames/FCN:LastCyrillic"/>&#160;
                            </td>
                            <td>
                              <xsl:value-of  select="ms:format-date(FIBD:ForeignCitizenData/FCD:BirthDate, 'dd.MM.yyyy') "/> г.
                            </td>
                            <td>
                              <xsl:for-each select="FIBD:Citizenships/FIBD:Citizenship">
                                <xsl:value-of  select="CH:CountryName"/>
                              </xsl:for-each>
                            </td>
                            <td>
                              <xsl:value-of select="FIBD:Identifier/."/>
                            </td>
                          </tr>
                        </xsl:for-each>
                        <tr>
                          <td>
                            1
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                        </tr>
                        <tr>
                          <td>
                            2
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                        </tr>
                        <tr>
                          <td>
                            3
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                        </tr>
                        <tr>
                          <td>
                            4
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                          <td>
                            ...
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td >
                      <table width="100%"  style="font-size:13px;border-collapse:collapse">
                        <tr style="border-bottom: 1pt solid black;">
                          <td colspan ="3">
                            <b>Долуподписаният родител, настойник, попечител или друг вид законен представител заявявам издаването на разрешение за пребиваване/документ за самоличност</b>
                          </td>
                        </tr>
                        <tr style="border-bottom: 1pt solid black;">
                          <td colspan ="2">
                            Имена:&#160;
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ParentTrusteeGuardianData/PTGD:PersonIdentificationData/ID:Names/NM:First"/>&#160;
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ParentTrusteeGuardianData/PTGD:PersonIdentificationData/ID:Names/NM:Middle"/>&#160;
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ParentTrusteeGuardianData/PTGD:PersonIdentificationData/ID:Names/NM:Last"/>&#160;
                          </td>
                          <td>
                            ЕГН/ЛНЧ:&#160;
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ParentTrusteeGuardianData/PTGD:PersonIdentificationData/ID:Identifier/."/>&#160;
                          </td>
                        </tr>
                        <tr>
                          <td >
                            № на документ:&#160;
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ParentTrusteeGuardianData/PTGD:IdentityDocumentBasicData/IDBD:IdentityNumber"/>&#160;
                          </td>
                          <td >
                            издаден на:&#160;
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ParentTrusteeGuardianData/PTGD:IdentityDocumentBasicData/IDBD:IdentitityIssueDate"/>&#160;
                          </td>
                          <td >
                            от &#160;
                            <xsl:value-of  select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ParentTrusteeGuardianData/PTGD:IdentityDocumentBasicData/IDBD:IdentityIssuer"/>&#160;
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td >
                      <table width="100%"  style="font-size:13px;border-collapse:collapse">
                        <tr>
                          <td>
                            <b>Издръжката се осигурява от:</b>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            Учреждение(организация):
                          </td>
                        </tr>
                        <tr>
                          <td>
                            Лице:
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td >
                      <table width="100%"  style="font-size:13px;border-collapse:collapse">
                        <tr>
                          <td>
                            <b>Лица под 14 години, придружаващи чужденеца и вписани в паспорта му</b>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            (над които чужденецът има родителски или настойнически права)
                          </td>
                        </tr>
                        <for-each select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ChildrensListedInForeignerPassport/AFIIDARPFRBD:ChildrenListedInForeignerPassport">
                          <tr>
                            <td>
                              <value-of select="CLFP:ForeignCitizenNames/FCN:FirstCyrillic"/>&#160;
                              <value-of select="CLFP:ForeignCitizenNames/FCN:OtherCyrillic"/>&#160;
                              <value-of select="CLFP:ForeignCitizenNames/FCN:LastCyrillic"/>&#160;
                            </td>
                          </tr>
                        </for-each>
                        <tr>
                          <td>
                            ..............................
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <table border="1" width="100%" cellspacing="0"  style="font-size:13px;">
                        <tr>
                          <td colspan ="2">
                            <b>Предишни пребивавания на чужденеца в Република България:</b>
                          </td>
                        </tr>
                        <tr>
                          <td >
                            За какъв период<br/>(от дата - до дата)
                          </td>
                          <td >
                            Адреси на пребиваване в Република България
                          </td>
                        </tr>
                        <tr>
                          <td >
                            &#160;
                          </td>
                          <td >
                            &#160;
                          </td>
                        </tr>
                        <tr>
                          <td >
                            &#160;
                          </td>
                          <td >
                            &#160;
                          </td>
                        </tr>
                        <tr>
                          <td >
                            &#160;
                          </td>
                          <td >
                            &#160;
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <b>Имате ли наложена принудителна административна мярка в Република България ? </b>&#160;
                      <xsl:choose>
                        <xsl:when test="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ImposedCompulsoryAdministrativeMeasure = true">
                          Да&#160;<input type="checkbox" disabled="" readonly="" />&#160;Не&#160;<input type="checkbox" disabled="" readonly="" />
                        </xsl:when>
                        <xsl:otherwise>
                          Да&#160;<input type="checkbox" disabled="" readonly="" />&#160;Не&#160;<input type="checkbox"  disabled="" readonly="" />
                        </xsl:otherwise>
                      </xsl:choose>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <table width="100%"  style="font-size:13px;">
                        <tr>
                          <td>
                            <b>Приел и регистрирал заявлението:</b>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            Дата:&#160;<xsl:value-of select="ms:format-date(AFIIDARPFRB:ElectronicAdministrativeServiceHeader/EASH:DocumentURI/DURI:ReceiptOrSigningDate , 'dd.MM.yyyy') "/> г.
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <b>Служебна информация, свързана с издаването на документа</b>&#160;
                      <xsl:value-of select="AFIIDARPFRB:ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData/AFIIDARPFRBD:ServiceInformation "/>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <xsl:choose>
              <xsl:when test = "AFIIDARPFRB:Declarations">
                <xsl:for-each select="AFIIDARPFRB:Declarations/AFIIDARPFRB:Declaration">
                  <xsl:choose>
                    <xsl:when test="DECL:IsDeclarationFilled = 'true'">
                      <tr>
                        <td colspan="2">
                          <xsl:value-of  select="DECL:DeclarationName" disable-output-escaping="yes" />
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
                Дата:&#160;<xsl:value-of  select="ms:format-date(AFIIDARPFRB:ElectronicAdministrativeServiceFooter/EASF:ApplicationSigningTime , 'dd.MM.yyyy')"/>г.
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

