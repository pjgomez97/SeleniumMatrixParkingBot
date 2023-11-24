module Shade

open System
open OpenQA.Selenium
open OpenQA.Selenium.Chrome
open OpenQA.Selenium.Firefox
open WebDriverManager
open WebDriverManager.DriverConfigs.Impl

type ElementType =
    | Button
    | Input
    | Link
    | Span
    | Any

let mutable driver : IWebDriver = null

let userAgent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.50 Safari/537.36"

let start browser =
    match browser with
    | "Firefox" ->
        let firefoxOptions = FirefoxOptions()
        firefoxOptions.AddArguments("--headless")
        DriverManager().SetUpDriver(FirefoxConfig()) |> ignore
        driver <- new FirefoxDriver(firefoxOptions)
    | "Chrome" ->
        let chromeOptions = ChromeOptions();
        chromeOptions.AddArgument("--headless=new")
        chromeOptions.AddArgument($"user-agent=%s{userAgent}")
        DriverManager().SetUpDriver(ChromeConfig()) |> ignore
        driver <- new ChromeDriver(chromeOptions)
    | _ ->
        raise <| InvalidOperationException("Unknown browser introduced")
        
    driver.Manage().Timeouts().ImplicitWait <- TimeSpan.FromSeconds 5

let refresh () =
    driver.Navigate().Refresh()

let quit () =
    driver.Quit()

let url url =
    driver.Url <- url

let element selector =
    driver.FindElement <| By.CssSelector selector

let elements selector =
    driver.FindElements <| By.CssSelector selector
    
let tag elementType =
    match elementType with
        | Button -> "button"
        | Input -> "input"
        | Link -> "a"
        | Span -> "span"
        | Any -> "*"

let elementByTextAndType text elementType =
    let tag = tag elementType

    driver.FindElement <| By.XPath ("//" + tag + "[contains(text(), '" + text + "')]")

let elementsByTextAndType text elementType =
    let tag = tag elementType

    driver.FindElements <| By.XPath ("//" + tag + "[contains(text(), '" + text + "')]")

let elementByText text =
    elementByTextAndType text ElementType.Any

let elementsByText text =
    elementsByTextAndType text ElementType.Any

let click (element: IWebElement) =
    element.Click()

let read (element: IWebElement) =
    element.Text

let write text (element: IWebElement) =
    element.SendKeys text

let getParent (element: IWebElement) =
    element.FindElement <| By.XPath ".."

let isSelected (element: IWebElement) =
    (element.GetAttribute "class").Split ""
    |> Seq.contains "selected"