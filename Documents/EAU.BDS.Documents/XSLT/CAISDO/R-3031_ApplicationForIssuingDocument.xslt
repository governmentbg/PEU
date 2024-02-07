<xsl:stylesheet version="1.0" xmlns:AID="http://ereg.egov.bg/segment/R-3031"
                xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
				        xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
				        xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
				        xmlns:P="http://ereg.egov.bg/segment/0009-000008"
				        xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
				        xmlns:ID="http://ereg.egov.bg/segment/0009-000006"
				        xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
				        xmlns:PA="http://ereg.egov.bg/segment/0009-000094"
				        xmlns:AIDD="http://ereg.egov.bg/segment/R-3038"
				        xmlns:PI="http://ereg.egov.bg/segment/R-3015"
				        xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
				        xmlns:DBIF="http://ereg.egov.bg/segment/R-3041"
				        xmlns:IBDIP="http://ereg.egov.bg/segment/R-3033"
				        xmlns:OICIBID="http://ereg.egov.bg/value/R-3034"
				        xmlns:DMST="http://ereg.egov.bg/segment/R-3040"
				        xmlns:SARD="http://ereg.egov.bg/segment/0009-000141"
						xmlns:DI="http://ereg.egov.bg/value/R-3250"
						xmlns:DCIC="http://ereg.egov.bg/nomenclature/R-1007"
                xmlns:EASF="http://ereg.egov.bg/segment/0009-000153"
                xmlns:DECL="http://ereg.egov.bg/segment//R-3136"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:ADD="http://ereg.egov.bg/segment/0009-000139"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				
xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >
	<xsl:include href="./BDSBaseTemplates.xslt"/>

	<xsl:output omit-xml-declaration="yes" method="html"/>
	<xsl:template match="AID:ApplicationForIssuingDocument">
		<xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
		<html>
			<xsl:call-template name="Head"/>
			<body>
				<table align="center" cellpadding="5" width= "700px">
					<thead>
						<tr>
							<th colspan ="2">
								<h2>
									<xsl:value-of select="AID:ElectronicAdministrativeServiceHeader/EASH:DocumentTypeName" />
								</h2>
							</th>
						</tr>
					</thead>
					<tbody>

						<xsl:choose>
							<xsl:when test="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType != 'R-1001'">
								<tr>
									<td colspan ="2">
										&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;От&#160;
										<xsl:value-of  select="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:First/."/>
										&#160;
										<xsl:choose>
											<xsl:when test="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Middle">
												<xsl:value-of  select="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Middle/."/>
												&#160;
											</xsl:when>
										</xsl:choose>
										<xsl:value-of  select="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Last/."/>

									</td>
								</tr>

							</xsl:when>
							<xsl:otherwise>
								<tr>
									<td colspan ="2">
										&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;От&#160;
										<xsl:value-of  select="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:First/."/>
										&#160;
										<xsl:choose>
											<xsl:when test="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle">
												<xsl:value-of  select="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/."/>
												&#160;
											</xsl:when>
										</xsl:choose>
										<xsl:value-of  select="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Last/."/>

									</td>
								</tr>
							</xsl:otherwise>
						</xsl:choose>
						<tr>
							<td colspan="2">
								&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;ЕГН/ЛНЧ&#160;
								<xsl:choose>
									<xsl:when test="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType != 'R-1001'">
										<xsl:value-of  select="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Identifier/."/>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of  select="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier/."/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
						</tr>
						<xsl:choose>
							<xsl:when test="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType != 'R-1001'">
								<tr>
									<td colspan ="2">
										&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Удостоверението се издава за&#160;
										<xsl:value-of  select="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:First/."/>
										&#160;
										<xsl:value-of  select="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/."/>
										&#160;
										<xsl:value-of  select="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Last/."/>
										&#160;ЕГН/ЛНЧ&#160;
										<xsl:value-of  select="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier/."/>

									</td>
								</tr>
							</xsl:when>
							<xsl:otherwise>
							</xsl:otherwise>
						</xsl:choose>
						<tr>
							<td colspan="2">
								&#160;&#160;&#160;&#160;&#160;&#160;&#160;
								<xsl:choose>
									<xsl:when test="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000087'">
										Лична карта
									</xsl:when>
								</xsl:choose>
								<xsl:choose>
									<xsl:when test="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000088'">
										Паспорт
									</xsl:when>
								</xsl:choose>
								<xsl:choose>
									<xsl:when test="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000089'">
										Дипломатически паспорт
									</xsl:when>
								</xsl:choose>
								<xsl:choose>
									<xsl:when test="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000090'">
										Служебен паспорт
									</xsl:when>
								</xsl:choose>
								<xsl:choose>
									<xsl:when test="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000091'">
										Моряшки паспорт
									</xsl:when>
								</xsl:choose>
								<xsl:choose>
									<xsl:when test="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000092'">
										Военна карта за самоличност
									</xsl:when>
								</xsl:choose>
								<xsl:choose>
									<xsl:when test="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000093'">
										Свидетелство за управление на моторно превозно средство
									</xsl:when>
								</xsl:choose>
								<xsl:choose>
									<xsl:when test="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000094'">
										Временен паспорт
									</xsl:when>
								</xsl:choose>
								<xsl:choose>
									<xsl:when test="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000095'">
										Служебен открит лист за преминаване на границата
									</xsl:when>
								</xsl:choose>
								<xsl:choose>
									<xsl:when test="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000096'">
										Временен паспорт за окончателно напускане на Република България
									</xsl:when>
								</xsl:choose>
								&#160;№&#160;<xsl:value-of  select="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityNumber"/><br/>
								&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;изд. на&#160;
								<xsl:value-of  select="ms:format-date(AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentitityIssueDate , 'dd.MM.yyyy')"/>г.
								&#160;от&#160;
								<xsl:value-of  select="AID:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityIssuer"/>
							</td>
						</tr>
						<tr>
							<td colspan = "2">
								&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Постоянен адрес:&#160;
								Обл.&#160;<xsl:value-of  select="AID:ApplicationForIssuingDocumentData/AIDD:PersonalInformation/PI:PersonAddress/PA:DistrictGRAOName"/>&#160;
								Общ.&#160;<xsl:value-of  select="AID:ApplicationForIssuingDocumentData/AIDD:PersonalInformation/PI:PersonAddress/PA:MunicipalityGRAOName"/>&#160;
								&#160;<xsl:value-of  select="AID:ApplicationForIssuingDocumentData/AIDD:PersonalInformation/PI:PersonAddress/PA:SettlementGRAOName"/>&#160;<br/>
								&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;<xsl:value-of  select="AID:ApplicationForIssuingDocumentData/AIDD:PersonalInformation/PI:PersonAddress/PA:StreetText"/>&#160;
								№&#160;<xsl:value-of  select="AID:ApplicationForIssuingDocumentData/AIDD:PersonalInformation/PI:PersonAddress/PA:BuildingNumber"/>&#160;
								вх.&#160;<xsl:value-of  select="AID:ApplicationForIssuingDocumentData/AIDD:PersonalInformation/PI:PersonAddress/PA:Entrance"/>&#160;
								ет.&#160;<xsl:value-of  select="AID:ApplicationForIssuingDocumentData/AIDD:PersonalInformation/PI:PersonAddress/PA:Floor"/>&#160;
								ап.&#160;<xsl:value-of  select="AID:ApplicationForIssuingDocumentData/AIDD:PersonalInformation/PI:PersonAddress/PA:Apartment"/>&#160;
							</td>

						</tr>
						<tr>
							<td>
								&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Телефон:&#160;
								<xsl:value-of  select="AID:ApplicationForIssuingDocumentData/AIDD:PersonalInformation/PI:MobilePhone"/>
							</td>

						</tr>
						<tr>
							<td colspan = "2">
								<p style="text-align:justify;">
									&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;На основание чл. 70, ал. 1, т. 2 и ал. 3 от Закона за българските лични документи (ЗБЛД) и чл. 53 от Правилника за издаване на българските лични документи, моля да бъде издадено удостоверение за:&#160;

									<xsl:choose>
										<xsl:when test="AID:ApplicationForIssuingDocumentData/AIDD:DocumentToBeIssuedFor/DBIF:IssuedBulgarianIdentityDocumentsInPeriod/.">
											издадени български лични документи през периода:<br/>
											&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;от&#160;<xsl:value-of  select="ms:format-date(AID:ApplicationForIssuingDocumentData/AIDD:DocumentToBeIssuedFor/DBIF:IssuedBulgarianIdentityDocumentsInPeriod/IBDIP:IdentitityIssueDate , 'dd.MM.yyyy')"/>г.&#160;
											до&#160;<xsl:value-of  select="ms:format-date(AID:ApplicationForIssuingDocumentData/AIDD:DocumentToBeIssuedFor/DBIF:IssuedBulgarianIdentityDocumentsInPeriod/IBDIP:IdentitityExpireDate , 'dd.MM.yyyy')"/>г.
										</xsl:when>
										<xsl:when test="AID:ApplicationForIssuingDocumentData/AIDD:DocumentToBeIssuedFor/DBIF:OtherIndormationConnectedWithIssuedBulgarianIdentityDocuments/.">
											събития и факти, свързани с издаването на български лични документи:<br/>
											<xsl:choose>
												<xsl:when test="AID:ApplicationForIssuingDocumentData/AIDD:DocumentToBeIssuedFor/DBIF:OtherIndormationConnectedWithIssuedBulgarianIdentityDocuments/OICIBID:NessesaryInformation/.">
													<xsl:value-of  select="AID:ApplicationForIssuingDocumentData/AIDD:DocumentToBeIssuedFor/DBIF:OtherIndormationConnectedWithIssuedBulgarianIdentityDocuments/OICIBID:NessesaryInformation"/>
												</xsl:when>
												<xsl:otherwise>
													<ul style="list-style: none;">
														<xsl:for-each select="AID:ApplicationForIssuingDocumentData/AIDD:DocumentToBeIssuedFor/DBIF:OtherIndormationConnectedWithIssuedBulgarianIdentityDocuments/OICIBID:DocumentNumbers/OICIBID:DocumentNumber">
															<xsl:if test=". != ''">
																<li>
																	Номер на документ:&#160;<xsl:value-of select="."></xsl:value-of>
																</li>
															</xsl:if>
														</xsl:for-each>
													</ul>
													<br/>
													<ul style="list-style: none;">
														<xsl:for-each select="AID:ApplicationForIssuingDocumentData/AIDD:DocumentToBeIssuedFor/DBIF:OtherIndormationConnectedWithIssuedBulgarianIdentityDocuments/OICIBID:DocumentsInfos/OICIBID:DocumentInfo">
															<xsl:if test="((DI:IssuingYear/. != '') and (DI:DocType/. != ''))">
																<li>
																	Година на издаване:&#160;<xsl:value-of select="DI:IssuingYear/."></xsl:value-of>&#160;Вид на документ:&#160;
																	<xsl:choose>
																		<xsl:when test="DI:DocType = 0">лична карта</xsl:when>
																		<xsl:when test="DI:DocType = 1">паспорт</xsl:when>
																		<xsl:otherwise>свидетелство за управление на МПС</xsl:otherwise>
																	</xsl:choose>
																</li>
															</xsl:if>
														</xsl:for-each>
													</ul>
													<ul style="list-style: none;">
														<xsl:for-each select="AID:ApplicationForIssuingDocumentData/AIDD:DocumentToBeIssuedFor/DBIF:OtherIndormationConnectedWithIssuedBulgarianIdentityDocuments/OICIBID:IncludsDataInCertificate/OICIBID:IncludedDataInCertificate">
															<li>
																<input type="checkbox" checked="true" readonly="true" />&#160;
																<xsl:choose>
																	<xsl:when test=". = 0">постоянен адрес (за лична карта)</xsl:when>
																	<xsl:when test=". = 1">имена на кирилица</xsl:when>
																	<xsl:when test=". = 2">имена на латиница</xsl:when>
																	<xsl:when test=". = 3">дата на издаване</xsl:when>
																	<xsl:when test=". = 4">дата на валидност</xsl:when>
																	<xsl:otherwise>актуален статус на документ и дата на обявяване</xsl:otherwise>
																</xsl:choose>
															</li>
														</xsl:for-each>
													</ul>
												</xsl:otherwise>
											</xsl:choose>
										</xsl:when>
									</xsl:choose>
								</p>
							</td>
						</tr>
						<tr>
							<td colspan = "2">
								&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Удостоверението е необходимо да послужи пред:
							</td>
						</tr>
						<tr>
							<td colspan = "2">
								<xsl:choose>
									<xsl:when test="AID:ApplicationForIssuingDocumentData/AIDD:DocumentToBeIssuedFor/DBIF:DocumentMustServeTo/DMST:InRepublicOfBulgariaDocumentMustServeTo">
										&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;<input type="checkbox" checked="true" disabled="" readonly="" />В Република България -&#160;
									</xsl:when>
									<xsl:otherwise>
										&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;<input type="checkbox"  disabled="" readonly="" />В Република България -&#160;
									</xsl:otherwise>
								</xsl:choose>
								<xsl:value-of  select="AID:ApplicationForIssuingDocumentData/AIDD:DocumentToBeIssuedFor/DBIF:DocumentMustServeTo/DMST:InRepublicOfBulgariaDocumentMustServeTo"/>
							</td>
						</tr>
						<tr>
							<td colspan = "2">
								<xsl:choose>
									<xsl:when test="AID:ApplicationForIssuingDocumentData/AIDD:DocumentToBeIssuedFor/DBIF:DocumentMustServeTo/DMST:AbroadDocumentMustServeTo">
										&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;<input type="checkbox" checked="true" disabled="" readonly="" />В чужбина -&#160;
									</xsl:when>
									<xsl:otherwise>
										&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;<input type="checkbox"  disabled="" readonly="" />В чужбина -&#160;
									</xsl:otherwise>
								</xsl:choose>
								<xsl:value-of  select="AID:ApplicationForIssuingDocumentData/AIDD:DocumentToBeIssuedFor/DBIF:DocumentMustServeTo/DMST:AbroadDocumentMustServeTo"/>
							</td>
						</tr>
						<tr>
							<td colspan = "2">
								<p style="text-align:justify;">
									&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Издаденото удостоверение да е в:&#160;
									<xsl:choose>
										<xsl:when test="AID:ServiceApplicantReceiptData/SARD:ServiceResultReceiptMethod='0006-000076'">
											<input type="checkbox" checked="true" disabled="" readonly="" />&#160;електронен формат
										</xsl:when>
										<xsl:otherwise>
											<input type="checkbox"  disabled="" readonly="" />&#160;електронен формат
										</xsl:otherwise>
									</xsl:choose>
									<xsl:choose>
										<xsl:when test="AID:ServiceApplicantReceiptData/SARD:ServiceResultReceiptMethod='R-6001'">
											<input type="checkbox" checked="true" disabled="" readonly="" /> на хартия, същото да бъде издадено по
										</xsl:when>
										<xsl:otherwise>
											<input type="checkbox"  disabled="" readonly="" />на хартия, същото да бъде издадено по
										</xsl:otherwise>
									</xsl:choose>
									<xsl:choose>
										<xsl:when test="AID:ApplicationForIssuingDocumentData/AIDD:AddressForIssuing=1">
											<input type="checkbox" checked="true" disabled="" readonly="" /> постоянния адрес
										</xsl:when>
										<xsl:otherwise>
											<input type="checkbox"  disabled="" readonly="" />постоянния адрес
										</xsl:otherwise>
									</xsl:choose>
									<xsl:choose>
										<xsl:when test="AID:ApplicationForIssuingDocumentData/AIDD:AddressForIssuing=2">
											<input type="checkbox" checked="true" disabled="" readonly="" /> настоящия адрес и желая да го получа в  звено „Български документи за самоличност” при
											<xsl:choose>
												<xsl:when test="AID:ServiceApplicantReceiptData/SARD:UnitInAdministration/SARD:AdministrativeDepartmentName">
													<xsl:value-of  select="AID:ServiceApplicantReceiptData/SARD:UnitInAdministration/SARD:AdministrativeDepartmentName"/>
												</xsl:when>
												<xsl:otherwise>
													...
												</xsl:otherwise>
											</xsl:choose>
										</xsl:when>
										<xsl:otherwise>
											<input type="checkbox"  disabled="" readonly="" />настоящия адрес и желая да го получа в  звено „Български документи за самоличност” при
											<xsl:choose>
												<xsl:when test="AID:ServiceApplicantReceiptData/SARD:UnitInAdministration/SARD:AdministrativeDepartmentName">
													<xsl:value-of  select="AID:ServiceApplicantReceiptData/SARD:UnitInAdministration/SARD:AdministrativeDepartmentName"/>
												</xsl:when>
												<xsl:otherwise>
													...
												</xsl:otherwise>
											</xsl:choose>
										</xsl:otherwise>
									</xsl:choose>
								</p>
							</td>
						</tr>
						<xsl:choose>
							<xsl:when test = "AID:Declarations">
								<xsl:for-each select="AID:Declarations/AID:Declaration">
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
						<xsl:choose>
							<xsl:when test = "AID:AttachedDocuments">
								<tr>
									<td colspan="2">
										Приложени документи:
									</td>
								</tr>
								<tr>
									<td colspan="2">
										<ol>
											<xsl:for-each select="AID:AttachedDocuments/AID:AttachedDocument">
												<li>
													<xsl:value-of select="ADD:AttachedDocumentDescription" />
												</li>
											</xsl:for-each>
										</ol>
									</td>
								</tr>
							</xsl:when>
						</xsl:choose>
						<tr>
							<td  colspan = "2">
								<p style="text-align:justify;">
									<b>ЗАБЕЛЕЖКА:</b> 1. Удостоверението ще бъде издадено след заплащане на държавна такса по Тарифа № 4 за таксите, които се събират в системата на МВР по Закона за държавните такси;
								</p>
							</td>
						</tr>
						<tr>
							<td  colspan = "2">
								<p style="text-align:justify;">2. Удостоверение, което ще послужи в чужбина, подлежи на допълнителна заверка и се издава само на хартия;</p>
							</td>
						</tr>
						<tr>
							<td  colspan = "2">
								<p style="text-align:justify;">3. Удостоверение по реда на чл. 70, ал. 3 от ЗБЛД се издава на хартия и при получаването му се представя оригинал на съдебно удостоверение и/или нотариално заверено изрично пълномощно.</p>
							</td>
						</tr>
						<tr>
							<td width="50%">
								Дата:&#160;<xsl:value-of  select="ms:format-date(AID:ElectronicAdministrativeServiceFooter/EASF:ApplicationSigningTime , 'dd.MM.yyyy')"/>&#160;г.
							</td>
							<td width="50%">								
							</td>
						</tr>
					</tbody>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
