
export enum gameType {
    Normal = "Normal",
    Timeout = "Timeout",
    Surrender = "Surrender",
    Quit = "Quit",
    Unknow = "Unknow"
}

export enum gameResult {
    X = "X win",
    Y = "Y win",
    Draw = "Draw",
    Null = "Null"
}

export function getGameType(id? :number) {
    switch (id) {
        case 0: {
            return gameType.Normal;;
        }
        case 1: {
            return gameType.Timeout; 
        }
        case 2: {
            return gameType.Surrender;
        }
        case 3: {
            return gameType.Quit;
        }
        default: {
            return gameType.Unknow;
        }
    } 
}

export function getGameResult(id?: number) {
    switch (id) {
        case 0: {
            return gameResult.X;;
        }
        case 1: {
            return gameResult.Y;;
        }
        case 2: {
            return gameResult.Draw;
        }

        default: {
            return gameResult.Null;
        }
    }
}