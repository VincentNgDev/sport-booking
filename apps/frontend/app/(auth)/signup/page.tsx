"use client";

import {
  AppForm,
  AppFormFieldInput,
  AppFormSubmit,
} from "@/components/apps/app-form/app-form";
import { Label } from "@/components/ui/label";
import Link from "next/link";

export default function SignUpPage() {
  return (
    <>
      <AppForm className="w-full space-y-4">
        <AppFormFieldInput propertyName={"Name"} caption="Full Name" />
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
        <AppFormFieldInput
          propertyName=""
          caption="Confirm Password"
          type="password"
          isRequiredField
        />
        <AppFormSubmit
          className="w-full font-semibold"
          Loading={() => "Signing up..."}
        >
          Sign Up
        </AppFormSubmit>
      </AppForm>
      <Label className="my-6 flex justify-center">
        Already have account?{" "}
        <Link
          href={"/login"}
          className="text-primary underline-offset-4 hover:underline"
        >
          Sign In
        </Link>
      </Label>
    </>
  );
}
