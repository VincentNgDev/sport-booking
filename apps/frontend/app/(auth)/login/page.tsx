"use client";

import {
  AppForm,
  AppFormFieldInput,
  AppFormSubmit,
} from "@/components/apps/app-form/app-form";
import { Label } from "@/components/ui/label";
import Link from "next/link";

export default function LoginPage() {
  return (
    <>
      <AppForm className="w-full space-y-4">
        <AppFormFieldInput
          propertyName=""
          caption="Email"
          type="email"
          isRequiredField
        />
        <AppFormFieldInput
          propertyName=""
          caption="Password"
          type="password"
          isRequiredField
        />
        <AppFormSubmit
          className="w-full font-semibold"
          Loading={() => "Signing in..."}
        >
          Sign In
        </AppFormSubmit>
      </AppForm>
      <Label className="my-6 flex justify-center">
        Don't have account?{" "}
        <Link
          href={"/signup"}
          className="text-primary underline-offset-4 hover:underline"
        >
          Sign Up
        </Link>
      </Label>
    </>
  );
}
