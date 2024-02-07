<xsl:stylesheet version="1.0" xmlns:AFWS="http://ereg.egov.bg/segment/R-3059"
						xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
				        xmlns:AFWSD="http://ereg.egov.bg/segment/R-3060"
						xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
						xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
						xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
						xmlns:P="http://ereg.egov.bg/segment/0009-000008"
				        xmlns:ID="http://ereg.egov.bg/segment/0009-000099"
						xmlns:C="http://ereg.egov.bg/segment/R-3252"
				        xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
				        xmlns:EASF="http://ereg.egov.bg/segment/0009-000153"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

	<xsl:include href="./BaseTemplates.xslt"/>
	<xsl:include href="./SignatureBaseTemplates.xslt"/>
	<xsl:param name="SignatureXML"></xsl:param>
	<xsl:param name="ApplicationPath"></xsl:param>

	<xsl:output omit-xml-declaration="yes" method="html"/>
	<xsl:template match="AFWS:ApplicationForWithdrawService">
		<xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
		<html>
			<xsl:call-template name="HeadNew">
				<xsl:with-param name="title">Заявление за отказ от заявена услуга</xsl:with-param>
			</xsl:call-template>
			<body>
				<div class="print-document flex-container">
					<div class="document-section document-header">
						<div class="flex-row row-table">
							<div class="flex-col"></div>
							<div class="flex-col">
								<p class="text-bold text-uppercase">
									Приложение № 10
								</p>
								<p class="text-bold text-uppercase">
									До<br/>
										Директора На<br/>
									Главна Дирекция „национална Полиция“
								</p>
							</div>
						</div>
						<h1 class="text-center document-title">ЗАЯВЛЕНИЕ</h1>
					</div>
					<div class="document-section">
						<p>
							От:&#160;<xsl:value-of  select="AFWS:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:First/."/>&#160;
						<xsl:if test="AFWS:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Middle">
							<xsl:value-of  select="AFWS:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Middle/."/>&#160;
						</xsl:if>
						<xsl:value-of  select="AFWS:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Last/."/>,&#160; 
							ЕГН/ЛНЧ/ЛН:&#160;<xsl:value-of  select="AFWS:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Identifier/."/>
						</p>
						<p>
							Лична карта №:&#160;<xsl:value-of  select="AFWS:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentityNumber/."/>
							<br/>
							изд. на:&#160;<xsl:value-of disable-output-escaping="yes" select="xslExtension:FormatDate(AFWS:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentitityIssueDate/., 'dd.MM.yyyy')"/>&#160;от&#160;<xsl:value-of  select="AFWS:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentityIssuer/."/>
						</p>
						<p>Адрес на електронна поща:&#160;<xsl:value-of  select="AFWS:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:EmailAddress"/>
					</p>
					</div>
					<div class="document-section">
						<p class="text-indent">Господин Директор,</p>
						<p class="text-indent text-justify">
							Заявявам Ви, че оттеглям заявлението си за издаване на следния документ:
						</p>
						<p class="text-bold">Разрешение за съхранение на огнестрелни оръжия и боеприпаси за тях от физически лица</p>
						<p>
							Данни, специфични за издавания документ:
						</p>
						<p>УРИ на преписка:&#160;<xsl:value-of  select="AFWS:ApplicationForWithdrawServiceData/AFWSD:CaseFileURI"/></p>
						<p>Причини за отказа:&#160;<xsl:value-of  select="AFWS:ApplicationForWithdrawServiceData/AFWSD:RefusalReasons"/>
					</p>
					</div>
					<div class="document-section">
						<div class="flex-row">
							<div class="flex-col">
								<p>Дата:&#160;<xsl:value-of  select="ms:format-date(AFWS:ElectronicAdministrativeServiceFooter/EASF:ApplicationSigningTime , 'dd.MM.yyyy')"/>г.</p>
							</div>
							<div class="flex-col">
								<xsl:call-template name="DocumentSignatures">
									<xsl:with-param name="Signatures" select = "$SignatureXML/DocumentSignatures" />
								</xsl:call-template>
							</div>
						</div>
					</div>
				</div>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
