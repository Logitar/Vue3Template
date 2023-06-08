import type { App } from "vue";
import MazPhoneNumberInput from "maz-ui/components/MazPhoneNumberInput";

export default function (app: App) {
  app.component("maz-phone-number-input", MazPhoneNumberInput);
}
