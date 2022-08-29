<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output method="html"/>
  <xsl:template name="getFilename">
    <xsl:param name="filePath" />
    <xsl:variable name="rest-of" select="substring-after($filePath, '\')" />
    <xsl:choose>
        <xsl:when test="contains($rest-of, '\')">
            <xsl:call-template name="getFilename">
                <xsl:with-param name="filePath" select="$rest-of" />
            </xsl:call-template>
        </xsl:when>
        <xsl:otherwise>
            <xsl:value-of select="$rest-of" />
        </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template match="R">

    <HTML>
      <HEAD>
        <TITLE>Test Report Large</TITLE>
      </HEAD>

      <BODY
       oncontextmenu="return false"
       background=".\TestReport.jpg"
       onload="if ( parent.adjustIFrameSize ) parent.adjustIFrameSize( window ) ;" >

        <table WIDTH="100%">
          <TD WITH="70%">
            <img src="./TestReport.gif"/>
          </TD>
        </table>
        <hr/>
        <H2 ALIGN="center"> Test Report </H2>

        <TABLE BORDER="1" WIDTH="100%">
          <TR>
            <TD WITH="50%">
              <TABLE border="0">
                <TR>
                  <TD>
                    <I>Board Code:</I>
                  </TD>
                  <TD>
                    <B>
                      <xsl:value-of select="ST/@BC"/>
                    </B>
                  </TD>
                </TR>
                <TR>
                  <TD>
                    <I>Board Name:</I>
                  </TD>
                  <TD>
                    <B>
                      <xsl:value-of select="ST/@NM"/>
                    </B>
                  </TD>
                </TR>
              </TABLE>
            </TD>

            <TD WITH="50%">

              <TABLE border="0">
                <TR>
                  <TD>
                    <I>Date:</I>
                  </TD>
                  <TD>
                    <B>
                      <xsl:value-of select="ST/@SD"/>
                    </B>
                  </TD>
                </TR>
                <TR>
                  <TD>
                    <I>Operator:</I>
                  </TD>
                  <TD>
                    <B>
                      <xsl:value-of select="ST/@OP"/>
                    </B>
                  </TD>
                </TR>
                <TR>
                  <TD>
                    <I> Test Method:&#160;&#160; </I>
                  </TD>
                  <TD>
                    <B>
                      <xsl:value-of select="ST/@TS"/>
                    </B>
                  </TD>
                  <BR></BR>
                </TR>
              </TABLE>
            </TD>
          </TR>
        </TABLE>

        <html>
          <br></br>
          <xsl:choose>
            <xsl:when test="ST/@KR = 0 ">
              Report Type: Only failed tested.
            </xsl:when>

            <xsl:when test="ST/@KR = 1 ">
              Report Type: All component tested.
            </xsl:when>

            <xsl:otherwise>

            </xsl:otherwise>
          </xsl:choose>
          <hr/>

          <xsl:variable  name = "MaxPrtErr" >
               <xsl:choose>
                    <xsl:when  test = "ST/@ME">
                         <xsl:value-of select = "ST/@ME"/>
                    </xsl:when>
                    <xsl:otherwise>
                         0
                    </xsl:otherwise>
               </xsl:choose>
          </xsl:variable>

          <xsl:variable  name = "ShowImage" >
               <xsl:choose>
                    <xsl:when  test = "ST/@SI">
                         <xsl:value-of select = "ST/@SI"/>
                    </xsl:when>
                    <xsl:otherwise>
                         0
                    </xsl:otherwise>
               </xsl:choose>
          </xsl:variable>

          <!--   <xsl:value-of select="$MaxPrtErr" /> -->

        <xsl:for-each select = "ET">
               <!-- <MLC Fri 20 Jan 2006> {E0398B1F-C0EC-4980-B46B-BE25239A0380} -->
               <xsl:for-each select = "WARNING">
                    <TR>
                    <TD>
                              <font size="-1" color="TOMATO">
                                   <xsl:if test="@MaxTestedFailBoards != 0">
                                        <xsl:text>&#160; ( Warning Maximum Board Errors : &#160;</xsl:text>
                                        <B> <xsl:value-of select="@MaxTestedFailBoards"/> </B> <xsl:text>&#160;)</xsl:text>
                                        <BR></BR>
                                        <xsl:text>&#160; ( Warning Panel Not Complete Tested ) &#160;</xsl:text>
                                        <BR></BR>
                                   </xsl:if>
                              </font>
                         </TD>
                    </TR>
               </xsl:for-each>
               <!-- </MLC Fri 20 Jan 2006> {E0398B1F-C0EC-4980-B46B-BE25239A0380} -->
          </xsl:for-each>

          <xsl:for-each select = "BI">
           <xsl:if test="count(TEST) > 0">

            <br></br>
            <table cellpadding="2" cellspacing="2" WIDTH="100%">
              <tr>
                <td>
                  <font color="BLUE">
                    Board No.: <xsl:value-of select="@ID+1"/>
                  </font>
                </td>

                <td width="10">
                </td>

                <xsl:if test="@BC != ''">
                  <td>
                    <font color="BLUE">
                      Board Code: <xsl:value-of select="@BC"/>
                    </font>
                  </td>
                </xsl:if>

                <td width="100">
                </td>

                <td>
                  <font color="BLUE">
                    Tested: <xsl:value-of select="@NT"/>
                  </font>
                </td>

                <td width="5">
                </td>

                <td>
                  <font color="BLUE">
                    Failed: <xsl:value-of select="@NF"/>
                  </font>
                </td>

              </tr>
            </table>


            <table cellpadding="2" cellspacing="2" WIDTH="100%">

              <xsl:for-each select = "TEST">

                <xsl:variable  name = "PrintedErr"  select = "position()"/>

               <!--   <xsl:value-of select="MaxPrtErr" />   -->
               <!--   <xsl:value-of select="$PrintedErr" />   -->

                <xsl:if test="$PrintedErr &lt;= $MaxPrtErr or $MaxPrtErr = 0">

                        <tr>

                         <td>
                            <xsl:value-of select="@SC"/>
                            <xsl:text>&#160;&#160;&#160;</xsl:text>
                            <!-- inserimento spazio bianco -->
                          </td>

                          <td>
                            <xsl:if test="@TR > 0">
                              <font color="RED">
                                <B>
                                  <xsl:value-of select="@NM"/>
                                </B>
                                <xsl:text>&#160;&#160;&#160;</xsl:text>
                                <!-- inserimento spazio bianco -->
                              </font>
                            </xsl:if>
                            <xsl:if test="@TR = 0">
                              <font color="GREEN">
                                <B>
                                  <xsl:value-of select="@NM"/>
                                </B>
                                <xsl:text>&#160;&#160;&#160;</xsl:text>
                                <!-- inserimento spazio bianco -->
                              </font>
                            </xsl:if>
                          </td>

                          <td>
                            <xsl:choose>
                              <xsl:when test="@MR = '1.79769e+308'">
                                Overflow
                              </xsl:when>
                              <xsl:when test="@MR = '1.79769e-308'">
                                Underflow
                              </xsl:when>
                              <xsl:otherwise>
                                <xsl:value-of select="@MR"/>
                                <xsl:text>&#160;&#160;</xsl:text>
                                <!-- inserimento spazio bianco -->
                              </xsl:otherwise>
                            </xsl:choose>
                          </td>

                          <td>
                            <xsl:value-of select="@MU"/>
                            <xsl:text>&#160;&#160;</xsl:text>
                            <!-- inserimento spazio bianco -->
                          </td>

                          <td>
                            <xsl:value-of select="@LB"/>
                            <xsl:text>&#160;&#160;</xsl:text>
                            <!-- inserimento spazio bianco -->
                          </td>


                          <td>
                            (  <xsl:value-of select="@ML"/><xsl:text>&#160;&#160;</xsl:text>
                            <xsl:value-of select="@MM"/><xsl:text>&#160;&#160;</xsl:text>
                            <xsl:value-of select="@MH"/>  ).<xsl:text>&#160;&#160;</xsl:text>
                          </td>

                          <td>
                             <xsl:if test="@TR > 0  and @MU != 'E'">
                              <font color="RED">
                                <B>
                                  Failed: <xsl:value-of select="@MP"/> .
                                </B>
                              </font>
                            </xsl:if>
                            <!--   <xsl:3.08.2011lau regolaPeter: scrivere solo Fail o solo Pass se Unit = E" />   -->
                            <xsl:if test="@TR > 0 and @MU = 'E'">
                              <font color="RED">
                                <I> Failed </I>
                                <xsl:text>&#160;</xsl:text>
                              </font>
                            </xsl:if>

                            <xsl:if test="@TR = 0 and @MU != 'E'">
                              <font color="GREEN">
                                <B>
                                  Passed: <xsl:value-of select="@MP"/> .
                                </B>
                              </font>
                            </xsl:if>

                            <xsl:if test="@TR = 0 and @MU = 'E'">
                              <font color="GREEN">
                                <B>
                                  Passed
                                </B>
                              </font>
                            </xsl:if>

                            <xsl:if test="@TR = -1 and @MU != 'E' ">
                              <font color="GREEN">
                                <B>
                                  Passed: <xsl:value-of select="@MP"/> .
                                </B>
                              </font>
                            </xsl:if>
                           <xsl:if test="@TR = -1 and @MU = 'E' ">
                              <font color="GREEN">
                                <B>
                                  Passed
                                </B>
                              </font>
                            </xsl:if>
                          </td>

                        </tr>

                        <xsl:if test="@IN != ''">
                          <TR>
                            <TD>
                              <PRE>
                                <xsl:text>&#160;&#160;</xsl:text>
                                <xsl:value-of select="@IN"/>
                              </PRE>
                            </TD>
                          </TR>
                        </xsl:if>
                        <xsl:if test="@FR != ''">
                          <TR>
                            <TD>
                              <PRE>
                                <xsl:text>&#160;&#160;</xsl:text>
                                  <!-- Invertire l'abilitazione delle due linee seguenti per disabilitare la gestione colori o altro da utente -->
                                   <xsl:value-of disable-output-escaping="yes" select ="@FR" />
                                  <!--<xsl:value-of select="@FR"/> -->
                              </PRE>
                            </TD>
                          </TR>
                        </xsl:if>
                          <xsl:if test="@PIC != '' and $ShowImage != 0">
                            <TR>
                              <TD>
                                <PRE>
                                  <img width="320" height="240" alt="{@PIC}">
                                    <xsl:attribute name="src">
                                      <xsl:call-template name="getFilename">
                                          <xsl:with-param name="filePath" select="@PIC" />
                                      </xsl:call-template>
                                    </xsl:attribute>
                                  </img>
                                </PRE>
                              </TD>
                            </TR>
                          </xsl:if>

                          <!-- <xsl:if test="@F = 'FNode' or @F = 'Fan' or @F = 'Contact' or @F = 'Pan' or @F = 'Isolation' "> -->
                 <xsl:variable  name = "TestName"  select = "translate(@F,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVXYZ')"/>
                          <xsl:if test=" $TestName =  'FNODE' or $TestName = 'FAN' or $TestName = 'CONTACT' or $TestName = 'PAN' or $TestName = 'ISOLATION' or $TestName = 'SHORT' or $TestName = 'CONTINUITY' ">
                          <TR>
                            <TD WIDTH="100%">
                                <xsl:text>&#160; Pin1: &#160;&#160; </xsl:text>
                                <xsl:value-of select="@CP1"/>
                                <br/>
                                <xsl:text>&#160; Pin2: &#160;&#160; </xsl:text>
                                <xsl:value-of select="@CP2"/>
                            </TD>
                          </TR>
                        </xsl:if>
                  <!-- 110713gs gestione del campo diagnostica istruzioni -->
                  <xsl:if test="@DG > 0">
                    <TR>
                      <TD>
                        <xsl:if test="@DG = 1">
                          <font size="2">
                            <font face="Verdana">
                              <xsl:text>&#160;&#160;-- Missing Component</xsl:text>
                            </font>
                          </font>
                        </xsl:if>
                        <xsl:if test="@DG = 2">
                          <font size="2">
                            <font face="Verdana">
                              <xsl:text>&#160;&#160;-- Short on the Component</xsl:text>
                            </font>
                          </font>
                        </xsl:if>
                        <xsl:if test="@DG = 3">
                          <font size="2">
                            <font face="Verdana">
                              <xsl:text>&#160;&#160;-- Component out of tolerance</xsl:text>
                            </font>
                          </font>
                        </xsl:if>
                        <xsl:if test="@DG = 4">
                          <font size="2">
                            <font face="Verdana">
                              <xsl:text>&#160;&#160;-- Component out of positive tolerance</xsl:text>
                            </font>
                          </font>
                        </xsl:if>
                        <xsl:if test="@DG = 5">
                          <font size="2">
                            <font face="Verdana">
                              <xsl:text>&#160;&#160;-- Component out of negative tolerance</xsl:text>
                            </font>
                          </font>
                        </xsl:if>
                        <xsl:if test="@DG = 6">
                          <font size="2">
                            <font face="Verdana">
                              <xsl:text>&#160;&#160;-- Component out of positive tolerance or missing guarding</xsl:text>
                            </font>
                          </font>
                        </xsl:if>
                        <xsl:if test="@DG = 7">
                          <font size="2">
                            <font face="Verdana">
                              <xsl:text>&#160;&#160;-- Component out of negative tolerance or missing guarding</xsl:text>
                            </font>
                          </font>
                        </xsl:if>
                        <xsl:if test="@DG = 8">
                          <font size="2">
                            <font face="Verdana">
                              <xsl:text>&#160;&#160;-- Component reversed</xsl:text>
                            </font>
                          </font>
                        </xsl:if>
                        <xsl:if test="@DG = 9">
                          <font size="2">
                            <font face="Verdana">
                              <xsl:text>&#160;&#160;-- Pin not soldered</xsl:text>
                            </font>
                          </font>
                        </xsl:if>
                        <xsl:if test="@DG = 10">
                          <font size="2">
                            <font face="Verdana">
                              <xsl:text>&#160;&#160;-- Pin shorted to GND</xsl:text>
                            </font>
                          </font>
                        </xsl:if>
                        <xsl:if test="@DG = 11">
                          <font size="2">
                            <font face="Verdana">
                              <xsl:text>&#160;&#160;-- Pin shorted to VCC</xsl:text>
                            </font>
                          </font>
                        </xsl:if>
                        <xsl:if test="@DG = 12">
                          <font size="2">
                            <font face="Verdana">
                              <xsl:text>&#160;&#160;-- Pin shorted</xsl:text>
                            </font>
                          </font>
                        </xsl:if>
                        <xsl:if test="@DG = 13">
                          <font size="2">
                            <font face="Verdana">
                              <xsl:text>&#160;&#160;-- Bad Component</xsl:text>
                            </font>
                          </font>
                        </xsl:if>
                        <xsl:if test="@DG = 14">
                          <font size="2">
                            <font face="Verdana">
                              <xsl:text>&#160;&#160;-- Bad Component ( State On Fail )</xsl:text>
                            </font>
                          </font>
                        </xsl:if>
                        <xsl:if test="@DG = 15">
                          <font size="2">
                            <font face="Verdana">
                              <xsl:text>&#160;&#160;-- Bad Component ( State Off Fail )</xsl:text>
                            </font>
                          </font>
                        </xsl:if>
                      </TD>
                    </TR>
                  </xsl:if>

                  <!-- 110713gs FINE gestione del campo diagnostica istruzioni -->

                </xsl:if>
              </xsl:for-each>

     <!-- <MLC Fri 20 Jan 2006> {E0398B1F-C0EC-4980-B46B-BE25239A0380} -->
                <xsl:for-each select = "WARNING">
                  <TR>
                    <TD>
                      <font size="-1" color="TOMATO">
                        <xsl:if test="@MaxPrintedErrors != 0">
                          <xsl:text>&#160; ( Warning Maximum Printed Errors : &#160;</xsl:text>
                          <B>
                            <xsl:value-of select="@MaxPrintedErrors"/>
                          </B>
                          <xsl:text>&#160;)</xsl:text>
                        </xsl:if>
                        <xsl:if test="@MaxWrittenErrors != 0">
                          <xsl:text>&#160; ( Warning Maximum Written Errors : &#160;</xsl:text>
                          <B>
                            <xsl:value-of select="@MaxWrittenErrors"/>
                          </B>
                          <xsl:text>&#160;)</xsl:text>
                        </xsl:if>
                        <xsl:if test="@MaxTestedErrors != 0">
                          <xsl:text>&#160; ( Warning Maximum Tested Errors : &#160;</xsl:text>
                          <B>
                            <xsl:value-of select="@MaxTestedErrors"/>
                          </B>
                          <xsl:text>&#160;)</xsl:text>
                        </xsl:if>
                      </font>
                    </TD>
                  </TR>
                </xsl:for-each>
     <!-- </MLC Fri 20 Jan 2006> {E0398B1F-C0EC-4980-B46B-BE25239A0380} -->

            </table>

           </xsl:if>
          </xsl:for-each>
        </html>

        <br></br>
        <TABLE BORDER="1" WIDTH="100%">
          <TR>
            <xsl:choose>
              <xsl:when test="ET/@NF != 0 ">
                <TD ALIGN="center" WITH="100%" >
                  <xsl:attribute name="style">background-color: RED</xsl:attribute>
                  Board Test Failed  -
                  Test nr: <xsl:value-of select="ET/@NT"/> -
                  Error nr: <xsl:value-of select="ET/@NF"/>
                </TD>
              </xsl:when>

              <xsl:when test="ET/@NT != 0 ">
                <TD ALIGN="center" WITH="100%" >
                  <xsl:attribute name="style">background-color: GREEN</xsl:attribute>
                  Board Test Passed  -
                  Test nr: <xsl:value-of select="ET/@NT"/> -
                  Error nr: <xsl:value-of select="ET/@NF"/>
                </TD>
              </xsl:when>

              <xsl:otherwise>
                <TD ALIGN="center" WITH="100%" >
                  <xsl:attribute name="style">background-color: BLUE</xsl:attribute>
                  Board Not Tested!!!!
                </TD>
              </xsl:otherwise>
            </xsl:choose>
          </TR>
        </TABLE>


      </BODY>

    </HTML>

  </xsl:template>

</xsl:stylesheet>
