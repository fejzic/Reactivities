import ActivityStore from "./activityStores";
import { useContext,createContext} from "react";

interface Store{
    activityStore : ActivityStore;

}

export const store: Store = {
    activityStore: new ActivityStore()

}

export const StoreContext = createContext(store);

export function useStore(){
    return useContext(StoreContext);
}

export default createContext(new ActivityStore());