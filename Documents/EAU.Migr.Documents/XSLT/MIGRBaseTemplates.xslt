<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xslExtension="urn:XSLExtension">

  <!-- Custom Variables -->

  <!-- Head Css Styles -->

  <xsl:template name="Head">

    <head>
      <style>
        .digital-stamp {
        display: inline-block;
        vertical-align: middle;
        padding: 0.3125rem;
        margin-left: 1.875rem;
        font-family: Roboto, Arial, "Segoe UI", "Helvetica Neue", Verdana, sans-serif;
        border: 0.1875rem solid #a0cade;
        border-radius: 0.625rem;
        }

        .digital-stamp .digital-stamp-body {
        display: flex;
        flex-direction: row;
        align-items: center;
        justify-content: space-between;

        border: 0.125rem solid #80b8d3;
        border-radius: 0.375rem;
        text-align: left;
        }

        .digital-stamp .digital-stamp-name {
        flex: 0 1 auto;
        padding: 0.3125rem 0.3125rem 0.3125rem 0.625rem;
        }

        .digital-stamp .digital-stamp-data {
        flex: 0 1 auto;
        padding: 0.3125rem 0.625rem 0.3125rem 0.3125rem;
        }
        .digital-stamp .digital-stamp-name-text {
        font-size: 1.125rem;
        font-weight: bold;
        line-height: 1;
        max-width: 9rem;
        word-wrap: break-word;
        }
        .digital-stamp .digital-stamp-data-text {
        font-size: 0.625rem;
        line-height: 1;
        max-width: 6rem;
        word-wrap: break-word;
        }

      </style>
    </head>
  </xsl:template>

  <!-- Base Templates -->

  <xsl:template name="DocumentSignatures">
    <xsl:param name="Signatures"/>
    <xsl:if test="$Signatures">
      <xsl:for-each select="$Signatures/Signature">
        <xsl:call-template name="DocumentSignature">
          <xsl:with-param name="Signature" select = "$Signatures/Signature" />
        </xsl:call-template>
      </xsl:for-each>
    </xsl:if>
  </xsl:template>

  <xsl:template name="DocumentSignature">
    <xsl:param name="Signature"/>
    <xsl:if test="$Signature">
      <div class="digital-stamp">
        <div class="digital-stamp-body">
          <div class="digital-stamp-name">
            <div class="digital-stamp-name-text">
              <xsl:value-of select="xslExtension:ExtractFirstGroup(SigningCertificateData/Subject/., 'CN=(.+?),')"/>
            </div>
          </div>
          <div class="digital-stamp-data">
            <div class="digital-stamp-data-text">
              Digitally signed by<br/>
              <xsl:value-of select="xslExtension:ExtractFirstGroup(SigningCertificateData/Subject/., 'CN=(.+?),')"/><br/>
              Date:&#32;
              <xsl:value-of disable-output-escaping="yes" select="xslExtension:FormatDate(SignatureTime/., 'yyyy.MM.dd&lt;\/br&gt;HH:mm:ss zzz')"/>
            </div>
          </div>
        </div>
      </div>
    </xsl:if>
  </xsl:template>

</xsl:stylesheet>