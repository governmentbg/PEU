<xsl:stylesheet version="1.0" xmlns:COVO="http://ereg.egov.bg/segment/R-3133"
								xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
								xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
								xmlns:P="http://ereg.egov.bg/segment/0009-000008"
								xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
								xmlns:FCN="http://ereg.egov.bg/segment/0009-000007"
								xmlns:ID="http://ereg.egov.bg/segment/0009-000006"
								xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
								xmlns:PA="http://ereg.egov.bg/segment/0009-000094"
								xmlns:PI="http://ereg.egov.bg/segment/R-3015"
								xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
								xmlns:DBIF="http://ereg.egov.bg/segment/R-3041"
								xmlns:IBDIP="http://ereg.egov.bg/segment/R-3033"
								xmlns:OICIBID="http://ereg.egov.bg/value/R-3034"
								xmlns:DMST="http://ereg.egov.bg/segment/R-3040"
								xmlns:SARD="http://ereg.egov.bg/segment/0009-000141"
								xmlns:VOIC="http://ereg.egov.bg/segment/R-3132"
								xmlns:I="http://ereg.egov.bg/segment/R-3131"
								xmlns:VOPFI="http://ereg.egov.bg/segment/0009-000011"
								xmlns:VOA="http://ereg.egov.bg/segment/R-3135"
								xmlns:E="http://ereg.egov.bg/segment/0009-000013"
								xmlns:VD="http://ereg.egov.bg/segment/R-3130"
								xmlns:VDI="http://ereg.egov.bg/segment/R-3128"
								xmlns:VDIC="http://ereg.egov.bg/segment/R-3129"
								xmlns:ESPBD="http://ereg.egov.bg/segment/0009-000002"
								xmlns:CURI="http://ereg.egov.bg/segment/0009-000044"
                xmlns:DURI="http://ereg.egov.bg/segment/0009-000001"
                xmlns:PD="http://ereg.egov.bg/segment/R-3037"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"

xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:include href="./KATBaseTemplates.xslt"/>

  <xsl:template match="COVO:CertificateOfVehicleOwnership">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>

      <body>
        <table align="center" cellpadding="5" width= "700px" >
          <tr>
            <th colspan ="3">
              <h3 class="uppercase">
                <xsl:value-of select="COVO:ElectronicServiceProviderBasicData/ESPBD:EntityBasicData/E:Name" />
              </h3>
            </th>
          </tr>
          <tr>
            <th colspan ="3">
              <h3>
                <u>
                  <xsl:value-of select="COVO:IssuingPoliceDepartment/PD:PoliceDepartmentName" />
                </u>
              </h3>
            </th>
          </tr>
          <tr>
            <th colspan ="3">
              <h2>
                &#160;
              </h2>
            </th>
          </tr>
          <tr>
            <th colspan ="3">
              <h3>
                Удостоверение №: <xsl:value-of select="COVO:CertificateNumber" />
              </h3>
            </th>
          </tr>
          <tr>
            <th colspan ="3">
              <h3>
                по преписка с №: <xsl:value-of select="COVO:AISCaseURI/CURI:DocumentURI/DURI:RegisterIndex" />-<xsl:value-of select="COVO:AISCaseURI/CURI:DocumentURI/DURI:SequenceNumber" />-<xsl:value-of  select="ms:format-date(COVO:AISCaseURI/CURI:DocumentURI/DURI:ReceiptOrSigningDate , 'dd.MM.yyyy')"/>
              </h3>
            </th>
          </tr>


          <tr>
            <td colspan ="3">
              &#160;Дава се настоящото на:&#160;
              <xsl:choose>
                <xsl:when test="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person">
                  <xsl:value-of  select="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:First/."/>
                  &#160;
                  <xsl:choose>
                    <xsl:when test="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/.">
                      <xsl:value-of  select="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/."/>&#160;
                    </xsl:when>
                  </xsl:choose>
                  <xsl:value-of  select="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Last/."/>
                </xsl:when>
              </xsl:choose>

              <xsl:choose>
                <xsl:when test="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity">
                  <xsl:value-of  select="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Name"/>
                </xsl:when>
              </xsl:choose>
            </td >
          </tr>
          <tr>
            <td colspan="3">
              <xsl:choose>
                <xsl:when test="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity">
                  ЕИК/БУЛСТАТ:&#160;<xsl:value-of  select="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Identifier"/>&#160;
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person">
                  ЕГН/ЛНЧ/ЛН:&#160;
                  <xsl:value-of  select="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier"/>&#160;
                </xsl:when>
              </xsl:choose>
            </td>
          </tr>
          <tr>
            <td colspan ="3">
              &#160;В уверение на това, че
              <xsl:choose>
                <xsl:when test="COVO:CertificateKind = '2001' ">
                  превозно средство с:
                  <table>
                    <xsl:for-each select="COVO:VehicleData/VD:VehicleDataItemCollection/VDIC:Items">
                      <tr>
                        <td colspan="3">
                          Регистрационен номер: <xsl:value-of  select="VDI:RegistrationNumber"/> <br/>
                          <xsl:choose>
                            <xsl:when test="VDI:PreviousRegistrationNumber">
                              Предишен регистрационен номер: <xsl:value-of  select="VDI:PreviousRegistrationNumber"/>
                            </xsl:when>
                          </xsl:choose>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">
                          Марка и модел:<xsl:value-of  select="VDI:MakeModel"/>&#160;
                        </td>
                        <td>
                          Рама:<xsl:value-of  select="VDI:IdentificationNumber"/>&#160;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">
                          Вид:<xsl:value-of  select="VDI:Type"/>&#160;
                        </td>
                        <td>
                          Двигател: <xsl:value-of  select="VDI:EngineNumber"/>&#160;
                        </td>
                      </tr>
                      <tr>

                        <td colspan="3">
                          Дата на първа регистрация: <xsl:value-of  select="ms:format-date(VDI:VehicleFirstRegistrationDate, 'dd.MM.yyyy')"/>&#160;
                        </td>
                      </tr>
                      <xsl:choose>
                        <xsl:when test="VDI:VehicleSuspension">
                          <tr >
                            <td colspan="3">Спиране от движение:</td>
                          </tr>
                          <xsl:for-each select="VDI:VehicleSuspension">
                            <tr>

                              <td colspan="3">
                                причина:&#160;<xsl:value-of  select="VDI:VehSuspensionReason"/> на дата:&#160;
                                <xsl:value-of  select="ms:format-date(VDI:VehSuspensionDate, 'dd.MM.yyyy')"/>
                              </td>
                            </tr>
                          </xsl:for-each>
                        </xsl:when>
                      </xsl:choose>
                    </xsl:for-each>
                    <xsl:variable name="j" select="count(COVO:VehicleOwnerInformationCollection/VOIC:Items)"/>
                    <tr>
                      <td colspan="3">

                        <table>
                          <tr>
                            <td colspan ="4">
                              Собственик(ци) са общо: <xsl:value-of  select="$j"/>
                            </td>
                          </tr>
                          <xsl:for-each select="COVO:VehicleOwnerInformationCollection/VOIC:Items">
                            <xsl:variable name="i" select="position()"/>
                            <xsl:choose>
                              <xsl:when test="$i > 4">
                              </xsl:when>
                              <xsl:otherwise>
                                <tr>
                                  <td>
                                    №<xsl:value-of select="$i"/>.&#160;
                                    <xsl:choose>
                                      <xsl:when test="I:VehicleOwnerCompanyInformation">
                                        ЕИК/БУЛСТАТ:<xsl:value-of  select="I:VehicleOwnerCompanyInformation/E:Identifier"/>&#160;
                                      </xsl:when>
                                    </xsl:choose>
                                    <xsl:choose>
                                      <xsl:when test="I:VehicleOwnerPersonalBGInformation">
                                        ЕГН/ЛНЧ/ЛН:&#160;<xsl:value-of  select="I:VehicleOwnerPersonalBGInformation/P:Identifier"/>&#160;
                                      </xsl:when>
                                    </xsl:choose>
                                    <br/>
                                    &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;<xsl:choose>
                                      <xsl:when test="I:VehicleOwnerCompanyInformation">
                                        <xsl:value-of  select="I:VehicleOwnerCompanyInformation/E:Name"/>&#160;
                                      </xsl:when>
                                    </xsl:choose>
                                    <xsl:choose>
                                      <xsl:when test="I:VehicleOwnerPersonalBGInformation">
                                        <xsl:value-of  select="I:VehicleOwnerPersonalBGInformation/P:Names/NM:First"/>&#160;
                                        <xsl:value-of  select="I:VehicleOwnerPersonalBGInformation/P:Names/NM:Middle"/>&#160;
                                        <xsl:value-of  select="I:VehicleOwnerPersonalBGInformation/P:Names/NM:Last"/>
                                      </xsl:when>
                                    </xsl:choose>
                                    <br/>
                                    &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Адрес:&#160;
                                    <xsl:choose>
                                      <xsl:when test="I:Address/VOA:DistrictName">
                                        ОБЛ.&#160;<xsl:value-of  select="I:Address/VOA:DistrictName"/>&#160;
                                      </xsl:when>
                                    </xsl:choose>
                                    <xsl:choose>
                                      <xsl:when test="I:Address/VOA:MunicipalityName">
                                        ОБЩ.&#160;<xsl:value-of  select="I:Address/VOA:MunicipalityName"/>&#160;
                                      </xsl:when>
                                    </xsl:choose>
                                    <xsl:choose>
                                      <xsl:when test="I:Address/VOA:ResidenceName">
                                        &#160;<xsl:value-of  select="I:Address/VOA:ResidenceName"/>&#160;<br/>
                                      </xsl:when>
                                    </xsl:choose>
                                    <xsl:choose>
                                      <xsl:when test="I:Address/VOA:AddressSupplement">
                                        <xsl:value-of  select="I:Address/VOA:AddressSupplement"/>&#160;<br/>
                                      </xsl:when>
                                    </xsl:choose>
                                  </td>
                                </tr>
                              </xsl:otherwise>
                            </xsl:choose>

                          </xsl:for-each>
                        </table>
                      </td>
                    </tr>

                    <xsl:choose>
                      <xsl:when test="$j>4" >
                        <tr>
                          <td colspan ="3">
                            Други собственици:&#160;
                            <xsl:for-each select="COVO:VehicleOwnerInformationCollection/VOIC:Items">
                              <xsl:variable name="i" select="position()"/>
                              <xsl:choose>
                                <xsl:when test="$i > 4">
                                  <xsl:choose>
                                    <xsl:when test="I:VehicleOwnerCompanyInformation">
                                      ЕИК/БУЛСТАТ:&#160;<xsl:value-of  select="I:VehicleOwnerCompanyInformation/E:Identifier"/>
                                    </xsl:when>
                                  </xsl:choose>
                                  <xsl:choose>
                                    <xsl:when test="I:VehicleOwnerPersonalBGInformation">
                                      ЕГН/ЛНЧ/ЛН:&#160;<xsl:value-of  select="I:VehicleOwnerPersonalBGInformation/P:Identifier"/>&#160;
                                    </xsl:when>
                                  </xsl:choose>
                                  <xsl:choose>
                                    <xsl:when test="position() != last()">,&#160;</xsl:when>
                                  </xsl:choose>
                                </xsl:when>




                              </xsl:choose>
                            </xsl:for-each>
                          </td>
                        </tr>
                      </xsl:when>
                    </xsl:choose>

                  </table>
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="COVO:CertificateKind = '2003' ">
                  следните превозни средства с:
                  <table width="100%">
                    <xsl:for-each select="COVO:VehicleData/VD:VehicleDataItemCollection/VDIC:Items">
                      <xsl:variable name="z" select="position()"/>
                      <tr>
                        <td colspan="2">
                          <xsl:value-of select="$z"/>.&#160;Регистрационен номер:&#160;<xsl:value-of  select="VDI:RegistrationNumber"/>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">
                          Марка и модел:&#160;<xsl:value-of  select="VDI:MakeModel"/>&#160;
                        </td>
                        <td>
                          Рама:&#160;<xsl:value-of  select="VDI:IdentificationNumber"/>&#160;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">
                          Вид:&#160;<xsl:value-of  select="VDI:Type"/>&#160;
                        </td>
                        <td>
                          Двигател:&#160;<xsl:value-of  select="VDI:EngineNumber"/>&#160;
                        </td>
                      </tr>
                      <xsl:choose>
                        <xsl:when test="VDI:VehicleFirstRegistrationDate">
                          <tr>
                            <td colspan = "3">
                              Дата на първа регистрация:&#160;<xsl:value-of  select="ms:format-date(VDI:VehicleFirstRegistrationDate, 'dd.MM.yyyy')"/>&#160;
                            </td>
                          </tr>
                        </xsl:when>
                      </xsl:choose>
                      <tr>
                        <td>
                          &#160;
                        </td>
                      </tr>
                    </xsl:for-each>
                    <tr>
                      <td colspan="3">&#160;</td>
                    </tr>
                    <xsl:for-each select="COVO:VehicleOwnerInformationCollection/VOIC:Items">
                      <tr>
                        <td colspan="3">
                          са собственост на:
                          <xsl:choose>
                            <xsl:when test="I:VehicleOwnerCompanyInformation">
                              &#160;<xsl:value-of  select="I:VehicleOwnerCompanyInformation/E:Name"/>&#160;

                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="I:VehicleOwnerPersonalBGInformation">
                              <xsl:value-of  select="I:VehicleOwnerPersonalBGInformation/P:Names/NM:First"/>&#160;
                              <xsl:value-of  select="I:VehicleOwnerPersonalBGInformation/P:Names/NM:Middle"/>&#160;
                              <xsl:value-of  select="I:VehicleOwnerPersonalBGInformation/P:Names/NM:Last"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3">
                          <xsl:choose>
                            <xsl:when test="I:VehicleOwnerCompanyInformation">
                              ЕИК/БУЛСТАТ:&#160;<xsl:value-of  select="I:VehicleOwnerCompanyInformation/E:Identifier"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="I:VehicleOwnerPersonalBGInformation">
                              ЕГН/ЛНЧ/ЛН:&#160;<xsl:value-of  select="I:VehicleOwnerPersonalBGInformation/P:Identifier"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3">
                          Адрес:&#160;
                          <xsl:choose>
                            <xsl:when test="I:Address/VOA:DistrictName">
                              ОБЛ.&#160;<xsl:value-of  select="I:Address/VOA:DistrictName"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="I:Address/VOA:MunicipalityName">
                              ОБЩ.&#160;<xsl:value-of  select="I:Address/VOA:MunicipalityName"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="I:Address/VOA:ResidenceName">
                              &#160;<xsl:value-of  select="I:Address/VOA:ResidenceName"/>&#160;<br/>
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="I:Address/VOA:AddressSupplement">
                              <xsl:value-of  select="I:Address/VOA:AddressSupplement"/>&#160;<br/>
                            </xsl:when>
                          </xsl:choose>
                        </td>
                      </tr>

                    </xsl:for-each>
                  </table>
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="COVO:CertificateKind = '2002' ">
                  превозно средство с:
                  <table width="100%">
                    <xsl:for-each select="COVO:VehicleData/VD:VehicleDataItemCollection/VDIC:Items">
                      <tr>
                        <td colspan="3">
                          Регистрационен номер: <xsl:value-of  select="VDI:RegistrationNumber"/>&#160;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">
                          Марка и модел:&#160;<xsl:value-of  select="VDI:MakeModel"/>&#160;
                        </td>
                        <td>
                          Рама:&#160;<xsl:value-of  select="VDI:IdentificationNumber"/>&#160;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">
                          Вид:&#160;<xsl:value-of  select="VDI:Type"/>&#160;
                        </td>
                        <td>
                          Двигател:&#160;<xsl:value-of  select="VDI:EngineNumber"/>&#160;
                        </td>
                      </tr>
                      <tr>

                        <td colspan = "3">&#160;</td>
                      </tr>
                    </xsl:for-each>

                    <xsl:for-each select="COVO:VehicleOwnerInformationCollection/VOIC:Items">
                      <tr>
                        <td colspan="3">
                          е било собственост на:
                          <xsl:choose>
                            <xsl:when test="I:VehicleOwnerCompanyInformation">
                              &#160;<xsl:value-of  select="I:VehicleOwnerCompanyInformation/E:Name"/>&#160;

                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="I:VehicleOwnerPersonalBGInformation">
                              <xsl:value-of  select="I:VehicleOwnerPersonalBGInformation/P:Names/NM:First"/>&#160;
                              <xsl:value-of  select="I:VehicleOwnerPersonalBGInformation/P:Names/NM:Middle"/>&#160;
                              <xsl:value-of  select="I:VehicleOwnerPersonalBGInformation/P:Names/NM:Last"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3">
                          <xsl:choose>
                            <xsl:when test="I:VehicleOwnerCompanyInformation">
                              ЕИК/БУЛСТАТ:&#160;<xsl:value-of  select="I:VehicleOwnerCompanyInformation/E:Identifier"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="I:VehicleOwnerPersonalBGInformation">
                              ЕГН/ЛНЧ/ЛН:&#160;<xsl:value-of  select="I:VehicleOwnerPersonalBGInformation/P:Identifier"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3">
                          Адрес:&#160;
                          <xsl:choose>
                            <xsl:when test="I:Address/VOA:DistrictName">
                              ОБЛ.&#160;<xsl:value-of  select="I:Address/VOA:DistrictName"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="I:Address/VOA:MunicipalityName">
                              ОБЩ.&#160;<xsl:value-of  select="I:Address/VOA:MunicipalityName"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="I:Address/VOA:ResidenceName">
                              &#160;<xsl:value-of  select="I:Address/VOA:ResidenceName"/>&#160;<br/>
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="I:Address/VOA:AddressSupplement">
                              <xsl:value-of  select="I:Address/VOA:AddressSupplement"/>&#160;<br/>
                            </xsl:when>
                          </xsl:choose>
                        </td>
                      </tr>

                    </xsl:for-each>
                    <tr>
                      <td colspan="3">

                        За периода от:&#160;<xsl:value-of  select="ms:format-date(COVO:VehicleData/VD:VehicleDataItemCollection/VDIC:Items/VDI:VehicleOwnerStartDate, 'dd.MM.yyyy')"/>&#160;г.&#160;
                        до:&#160;<xsl:value-of  select="ms:format-date(COVO:VehicleData/VD:VehicleDataItemCollection/VDIC:Items/VDI:VehicleOwnerEndDate, 'dd.MM.yyyy')"/>&#160;г.


                      </td>
                    </tr>
                  </table>
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="COVO:CertificateKind = '2005' ">
                  <b>няма данни</b> за собственост на ПС на:
                  <table width="100%">
                    <tr>
                      <td colspan = "2">
                        <xsl:choose>
                          <xsl:when test="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity">
                            <xsl:value-of  select="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Name/."/>
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:value-of  select="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:First/."/>
                            &#160;
                            <xsl:choose>
                              <xsl:when test="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/.">
                                <xsl:value-of  select="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/."/>&#160;
                              </xsl:when>
                            </xsl:choose>
                            <xsl:value-of  select="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Last/."/>
                          </xsl:otherwise>
                        </xsl:choose>
                      </td>
                    </tr>
                    <tr>
                      <td colspan = "2">
                        <xsl:choose>
                          <xsl:when test="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity">
                            ЕИК/БУЛСТАТ:&#160;<xsl:value-of  select="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Identifier"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person">
                            ЕГН/ЛНЧ/ЛН:&#160;
                            <xsl:value-of  select="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                      </td>
                    </tr>
                  </table>
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when  test="COVO:CertificateKind = '2004' or COVO:CertificateKind = '2006'">


                  <b>няма данни</b> превозно средство с:
                  <table width="100%">
                    <xsl:for-each select="COVO:VehicleData/VD:VehicleDataItem">
                      <tr>
                        <td colspan="2">
                          Регистрационен номер: <xsl:value-of  select="VDI:RegistrationNumber"/>&#160;,
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">
                          <xsl:choose>
                            <xsl:when test="VDI:PreviousRegistrationNumber">
                              Предишен регистрационен номер: <xsl:value-of  select="VDI:PreviousRegistrationNumber"/>
                            </xsl:when>
                          </xsl:choose>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          Марка и модел:<xsl:value-of  select="VDI:MakeModel"/>&#160;
                        </td>
                        <td>
                          Рама:<xsl:value-of  select="VDI:IdentificationNumber"/>&#160;
                        </td>
                      </tr>
                      <tr>
                        <td>
                          Вид:<xsl:value-of  select="VDI:Type"/>&#160;
                        </td>
                        <td>
                          Двигател: <xsl:value-of  select="VDI:EngineNumber"/>&#160;
                        </td>
                      </tr>
                      <tr>

                        <td>
                          Дата на първа регистрация: <xsl:value-of  select="ms:format-date(VDI:VehicleFirstRegistrationDate , 'dd.MM.yyyy')"/>&#160;
                        </td>
                      </tr>
                    </xsl:for-each>
                    <tr>
                      <td colspan = "2">
                        <xsl:choose>
                          <xsl:when test="COVO:CertificateKind = '2004'">
                            да е собственост на:
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="COVO:CertificateKind = '2006'">
                            да е бивша собственост на:
                          </xsl:when>
                        </xsl:choose>


                        <xsl:choose>
                          <xsl:when test="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity">
                            <xsl:value-of  select="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Name/."/>
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:value-of  select="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:First/."/>
                            &#160;
                            <xsl:choose>
                              <xsl:when test="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/.">
                                <xsl:value-of  select="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/."/>&#160;
                              </xsl:when>
                            </xsl:choose>
                            <xsl:value-of  select="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Last/."/>
                          </xsl:otherwise>
                        </xsl:choose>
                      </td>
                    </tr>
                    <tr>
                      <td colspan = "2">
                        <xsl:choose>
                          <xsl:when test="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity">
                            ЕИК/БУЛСТАТ:&#160;<xsl:value-of  select="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Identifier"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person">
                            ЕГН/ЛНЧ/ЛН:&#160;
                            <xsl:value-of  select="COVO:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                      </td>
                    </tr>
                  </table>

                </xsl:when>
              </xsl:choose>

            </td>
          </tr>
          <tr>
            <td colspan ="2">
              <p>
                Данните в удостоверението са актуални към&#160;
                <xsl:choose>
                  <xsl:when test="COVO:KATVerificationDateTime">
                    <xsl:value-of select="xslExtension:FormatDate(COVO:KATVerificationDateTime, 'dd.MM.yyyy')"/>&#160;г.&#160;
                    <xsl:value-of select="xslExtension:FormatDate(COVO:KATVerificationDateTime, 'HH:mm:ss')"/>&#160;ч.&#160;
                  </xsl:when>
                </xsl:choose>
              </p>
            </td>
          </tr>
          <tr>
            <td colspan ="2">
              &#160;Удостоверението да послужи: &#160;
              <xsl:choose>
                <xsl:when test="COVO:OwnershipCertificateReason = '2001'">
                  пред други
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="COVO:OwnershipCertificateReason = '2002'">
                  пред	застрахователни дружества
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="COVO:OwnershipCertificateReason = '2003'">
                  пред	консулски служби
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="COVO:OwnershipCertificateReason = '2004'">
                  за	министерство на транспорта
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="COVO:OwnershipCertificateReason = '2005'">
                  пред	митнически органи
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="COVO:OwnershipCertificateReason = '2006'">
                  пред  нотариус
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="COVO:OwnershipCertificateReason = '2007'">
                  пред	съдебни власти
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="COVO:OwnershipCertificateReason = '2008'">
                  пред финансови органи
                </xsl:when>
              </xsl:choose>
            </td>
          </tr>
          <tr>
            <td colspan ="2">
              &#160;
            </td>
          </tr>

          <tr>
            <td width="50%">
              Дата:
              <xsl:choose>
                <xsl:when test="COVO:DocumentReceiptOrSigningDate">
                  <xsl:value-of  select="ms:format-date(COVO:DocumentReceiptOrSigningDate , 'dd.MM.yyyy')"/>&#160;г.
                </xsl:when>
              </xsl:choose>
            </td>
            <td width="50%">

            </td>
          </tr>
          <tr>
            <td width="50%">
              &#160;
            </td>
            <td width="50%">
              <xsl:value-of select="COVO:Official/COVO:PersonNames/NM:First/." />&#160;<xsl:value-of select="COVO:Official/COVO:PersonNames/NM:Last/." />
            </td>
          </tr>
        </table>

      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
