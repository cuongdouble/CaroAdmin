import { createSlice, PayloadAction, Dispatch } from '@reduxjs/toolkit';
import UserService from '@Services/UserService';
import { GUID } from '../models/GuidType';
import { IUserModel } from '../models/IUserModel';

// Declare an interface of the store's state.
export interface IUserStoreState {
    isFetching: boolean;
    collection: IUserModel[];
}

// Create the slice.
const slice = createSlice({
    name: "user",
    initialState: {
        isFetching: false,
        collection: []
    } as IUserStoreState,
    reducers: {
        setFetching: (state, action: PayloadAction<boolean>) => {
            state.isFetching = action.payload;
        },
        setData: (state, action: PayloadAction<IUserModel[]>) => {
            state.collection = action.payload;
        },
        updateData: (state, action: PayloadAction<IUserModel>) => {
            var collection = [...state.collection];
            var entry = collection.find(x => x.id === action.payload.id);
            entry.bannedAt = action.payload.bannedAt;
            state.collection = [...state.collection];
        },
    }
});

// Export reducer from the slice.
export const { reducer } = slice;

// Define action creators.
export const actionCreators = {
    search: (term?: string) => async (dispatch: Dispatch) => {
        dispatch(slice.actions.setFetching(true));

        const service = new UserService();

        const result = await service.search(term);

        if (!result.hasErrors) {
            dispatch(slice.actions.setData(result.value));
        }

        dispatch(slice.actions.setFetching(false));

        return result;
    },

    ban: (model: IUserModel) => async (dispatch: Dispatch) => {
        dispatch(slice.actions.setFetching(true));
        const service = new UserService();

        const result = await service.update(model);

        if (!result.hasErrors) {
            dispatch(slice.actions.updateData(result.value));
        }

        dispatch(slice.actions.setFetching(false));

        return result;
    },
};
