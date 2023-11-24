module Logger

open FSLogger

let log = Logger.ColorConsole

let logInfo msg =
    log.I(" " + msg)
    
let logWarning msg =
    log.W(" " + msg)

let logError msg =
    log.E(" " + msg)