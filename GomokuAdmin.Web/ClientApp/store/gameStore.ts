import { createSlice, PayloadAction, Dispatch } from '@reduxjs/toolkit';
import GameService from '@Services/GameService';
import { GUID } from '../models/GuidType';
import { IGameModel } from '../models/IGameModel';

// Declare an interface of the store's state.
export interface IGameStoreState {
    isFetching: boolean;
    collection: IGameModel[];
}

// Create the slice.
const slice = createSlice({
    name: "game",
    initialState: {
        isFetching: false,
        collection: []
    } as IGameStoreState,
    reducers: {
        setFetching: (state, action: PayloadAction<boolean>) => {
            state.isFetching = action.payload;
        },
        setData: (state, action: PayloadAction<IGameModel[]>) => {
            state.collection = action.payload;
        },
    }
});

// Export reducer from the slice.
export const { reducer } = slice;

// Define action creators.
export const actionCreators = {
    search: (id?: GUID) => async (dispatch: Dispatch) => {
        dispatch(slice.actions.setFetching(true));

        const service = new GameService();
        const result = await service.search(id);

        if (!result.hasErrors) {
            dispatch(slice.actions.setData(result.value));
        }

        dispatch(slice.actions.setFetching(false));

        return result;
    },
};
